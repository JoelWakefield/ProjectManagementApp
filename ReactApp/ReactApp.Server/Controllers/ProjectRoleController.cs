using Microsoft.AspNetCore.Mvc;
using ReactApp.Server.Data;

namespace ReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectRoleController(ApplicationDbContext dbContext) : ControllerBase
    {
        private ApplicationDbContext dbContext = dbContext;

        [HttpGet(Name = "GetProjectRoles")]
        public IEnumerable<ProjectRole> GetRoles()
        {
            return dbContext.ProjectRoles;
        }
    }
}
