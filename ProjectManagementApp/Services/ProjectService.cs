using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class ProjectService(ApplicationDbContext dbContext)
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<Project> GetProjects() => dbContext.Projects;
        
        public Project? GetProject(string id) => dbContext.Projects.FirstOrDefault(p => p.Id == id);
        
        public void Create(Project project)
        {
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            Project? existingProject = GetProject(project.Id!);
            if (existingProject != null)
            {
                existingProject = project;
                dbContext.SaveChanges();
            }
        }
    }
}
