using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetProjects();
        Task<Project?> GetProjectAsync(string id);
        Task CreateAsync(Project project);
        Task UpdateProjectAsync(Project project);
    }

    public class ProjectService(ApplicationDbContext dbContext) : IProjectService
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<Project> GetProjects() => dbContext.Projects;
        
        public async Task<Project?> GetProjectAsync(string id) => await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
        
        public async Task CreateAsync(Project project)
        {
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            Project? existingProject = await GetProjectAsync(project.Id!);
            if (existingProject != null)
            {
                existingProject = project;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
