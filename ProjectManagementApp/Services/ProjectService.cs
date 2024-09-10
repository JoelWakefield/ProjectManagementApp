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
        Task<string> CreateAsync(CreateProjectVm project);
        Task UpdateProjectAsync(ProjectVm project);
    }

    public class ProjectService(ApplicationDbContext dbContext, IMapper mapper) : IProjectService
    {
        private ApplicationDbContext dbContext = dbContext;
        private IMapper mapper { get; set; } = mapper;

        public IEnumerable<ProjectVm> GetProjects() => mapper.Map<IEnumerable<Project>, IEnumerable<ProjectVm>>(dbContext.Projects);

        public async Task<ProjectVm?> GetProjectAsync(string id) => mapper.Map<ProjectVm>(
            await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id));

        public async Task<string> CreateAsync(CreateProjectVm projectVm)
        {
            Project project = mapper.Map<Project>(projectVm);

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task UpdateProjectAsync(ProjectVm projectVm)
        {
            Project project = mapper.Map<Project>(projectVm);
            dbContext.Projects.Attach(project);
            dbContext.Entry(project).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
