using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Data;
using ReactApp.Server.ViewModels;

namespace ReactApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController(ApplicationDbContext dbContext, IMapper mapper) : ControllerBase
    {
        private ApplicationDbContext dbContext = dbContext;
        private IMapper mapper = mapper;

        [HttpGet("GetProjects")]
        public IEnumerable<ProjectVm> GetProjects()
        {
            return dbContext.Projects
                .Include(p => p.Owner)
                .Select(p => mapper.Map<Project, ProjectVm>(p));
        }

    }
}
