using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class ProjectService(ApplicationDbContext dbContext)
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<Project> GetProjects() => dbContext.Projects;
        
        public Project? GetProject(string id) => dbContext.Projects.FirstOrDefault(p => p.Id == id);
        
        public async Task<Project?> GetProjectAsync(string id) => await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);

        public async Task CreateAsync(Project project)
        {
            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateProject(Project project)
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
