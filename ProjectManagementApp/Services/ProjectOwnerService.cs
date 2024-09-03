using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IProjectOwnerService
    {
		//Task<ApplicationUser?> GetOwnerAsync(string ownerId);
        Task AssignOwnerAsync(string projectId, string ownerId);
		Task AssignOwnerAsync(Project project, string ownerId);
	}

    public class ProjectOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IProjectOwnerService
    {
        private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager { get; set; } = userManager;

		//public async Task<ApplicationUser?> GetOwnerAsync(string userName) => await userManager.Users.FirstOrDefaultAsync(o => o.UserName == userName);

		public async Task AssignOwnerAsync(string projectId, string ownerId)
        {
            ApplicationUser? owner = userManager.Users.FirstOrDefault(u => u.Id == ownerId);
            Project? project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);
            
			await AssignOwnerAsync(project, owner);
        }

		public async Task AssignOwnerAsync(Project project, string ownerId)
		{
			ApplicationUser? owner = userManager.Users.FirstOrDefault(u => u.Id == ownerId);

			await AssignOwnerAsync(project, owner);
		}

		private async Task AssignOwnerAsync(Project? project, ApplicationUser? owner)
		{
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
