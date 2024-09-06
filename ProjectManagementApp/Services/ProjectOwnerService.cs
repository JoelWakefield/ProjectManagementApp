using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.SampleData;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IProjectOwnerService
    {
        IAsyncEnumerable<KanBanSection> GetOwnerSectionsAsync();
        IEnumerable<KanbanItem> GetProjectItems();
        Task AssignOwnerAsync(string projectId, string ownerId);
	}

    public class ProjectOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) : IProjectOwnerService
    {
        private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager { get; set; } = userManager;
        private IMapper mapper { get; set; } = mapper;

        public async IAsyncEnumerable<KanBanSection> GetOwnerSectionsAsync()
        {
            ProjectRole ? projectRole = await dbContext.ProjectRoles.FirstOrDefaultAsync(r => r.Name == "owner");

            if (projectRole != null)
            {
                IEnumerable<ApplicationUserVm> users = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserVm>>(projectRole.Users);

                foreach (var owner in users)
                    yield return new KanBanSection(owner!.UserName ?? "USERNAME NOT FOUND", true);
            }
        }

        /// <summary>
        /// Returns the project owners as KanbanItems
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KanbanItem> GetProjectItems()
        {
            // Assign each project to the owner's section
            foreach (var project in dbContext.Projects)
                yield return new KanbanItem(project.Name, project.Id, project.Owner.UserName, project.Owner.Id);
        }

        public async Task AssignOwnerAsync(string projectId, string ownerId)
        {
            ApplicationUser? owner = userManager.Users.FirstOrDefault(u => u.Id == ownerId);
            Project? project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null || owner == null)
            {
                return;
            }

            project.Owner = owner;
			await dbContext.ProjectOwners.AddAsync(new ProjectOwnerArchive()
			{
				ProjectId = project.Id,
				UserId = owner.Id,
			});
			await dbContext.SaveChangesAsync();
        }
	}
}
