using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Data;
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

        [HttpGet("{id}")]
        public async Task<ProjectVm> GetProject(string id)
        {
            var project = await dbContext.Projects
                .Include(p => p.Owner)
                .SingleAsync(p => p.Id == id);

            return mapper.Map<ProjectVm>(project);
        }
    }
}
