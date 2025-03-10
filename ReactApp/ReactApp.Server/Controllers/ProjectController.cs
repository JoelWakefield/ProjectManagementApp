using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Models;
using ReactApp.Server.ViewModels;

namespace ReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController(ApplicationDbContext dbContext, IMapper mapper) : ControllerBase
    {
        private ApplicationDbContext dbContext = dbContext;
        private IMapper mapper = mapper;

        [HttpGet(Name = "GetProjects")]
        public IEnumerable<ProjectVm> GetProjects()
        {
            return dbContext.Projects
                .Include(p => p.Owner)
                .Select(p => mapper.Map<Project, ProjectVm>(p));
        }

        [HttpPost("create")]
        public async Task<string> CreateProjectAsync(CreateProjectVm projectVm)
        {
            try
            {
                Project project = new();

                //  set flat project data
                project.CreatedBy = "Create Projects API";

                project.Name = projectVm.Name;
                project.Description = projectVm.Description;

                project.ProjectedStart = projectVm.ProjectedStart.ToDateTime(new TimeOnly(), DateTimeKind.Utc);
                project.ProjectedEnd = projectVm.ProjectedEnd.ToDateTime(new TimeOnly(), DateTimeKind.Utc);

                project.Owner = dbContext.Users
                    .FirstOrDefault(u => u.Id == projectVm.OwnerId)
                    ?? throw new Exception($"Cannot find user with id: {project.OwnerId}");
                project.OwnerId = projectVm.OwnerId;

                await dbContext.Projects.AddAsync(project);
                await dbContext.SaveChangesAsync();

                return project.Id;
            } 
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpGet("details/{id}")]
        public async Task<ProjectVm> GetProjectDetailsAsync(string id)
        {
            var project = await dbContext.Projects
                .Include(p => p.Owner)
                .SingleAsync(p => p.Id == id);

            return mapper.Map<ProjectVm>(project);
        }

        [HttpGet("edit/{id}")]
        public async Task<EditProjectVm> GetProjectEdit(string id)
        {
            var project = await dbContext.Projects
                .Include(p => p.Owner)
                .SingleAsync(p => p.Id == id);

            var vm = mapper.Map<EditProjectVm>(project);
            return vm;
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateProjectAsync(string id, EditProjectVm projectVm)
        {
            try
            {
                //  get project data
                var project = await dbContext.Projects
                    .SingleOrDefaultAsync(p => p.Id == id);
                if (project == null)
                {
                    return NotFound(new { message = $"Project with id ({id}) not found." });
                }

                //  update flat project data
                project.Name = projectVm.Name;
                project.Description = projectVm.Description;

                project.ProjectedStart = projectVm.ProjectedStart.ToDateTime(new TimeOnly(), DateTimeKind.Utc);
                project.ProjectedEnd = projectVm.ProjectedEnd.ToDateTime(new TimeOnly(), DateTimeKind.Utc);

                //  save project data
                await dbContext.SaveChangesAsync();
                return Ok(new { message = $" Project with id ({id}) successfully updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating project with id ({id})", error = ex.Message });
            }
        }
    }
}
