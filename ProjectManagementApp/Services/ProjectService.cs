using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectVm> GetProjects();
        Task<ProjectVm?> GetProjectAsync(string id);
        Task CreateAsync(ProjectVm project);
        Task UpdateProjectAsync(ProjectVm project);
    }

    public class ProjectService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) : IProjectService
    {
        private ApplicationDbContext dbContext = dbContext;
        private UserManager<ApplicationUser> userManager { get; set; } = userManager;
        private IMapper mapper { get; set; } = mapper;

        public IEnumerable<ProjectVm> GetProjects() => mapper.Map<IEnumerable<Project>, IEnumerable<ProjectVm>>(dbContext.Projects);

        public async Task<ProjectVm?> GetProjectAsync(string id) => mapper.Map<ProjectVm>(
            await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id));

        public async Task CreateAsync(ProjectVm projectVm)
        {
            Project project = new Project()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = Environment.UserName,
                CreatedOn = DateTime.UtcNow,

                Name = projectVm.Name,
                Description = projectVm.Description,
                ProjectedStart = projectVm.ProjectedStart,
                ProjectedEnd = projectVm.ProjectedEnd,
                ActualStart = projectVm.ActualStart,
                ActualEnd = projectVm.ActualEnd,
                TotalWorkHours = projectVm.TotalWorkHours,
            };

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(ProjectVm project)
        {
            Project? existingProject = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);
            if (existingProject != null)
            {
                existingProject.Name = project.Name;
                existingProject.Description = project.Description;
                existingProject.ProjectedStart = project.ProjectedStart;
                existingProject.ProjectedEnd = project.ProjectedEnd;
                existingProject.ActualStart = project.ActualStart;
                existingProject.ActualEnd = project.ActualEnd;
                existingProject.TotalWorkHours = project.TotalWorkHours;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
