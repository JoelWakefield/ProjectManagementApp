using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Data;
using ReactApp.Server.ViewModels;

namespace ReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(ApplicationDbContext dbContext) : ControllerBase
    {
        private ApplicationDbContext dbContext = dbContext;

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<UserDetails> GetUsers()
        {
            return dbContext.Users.Include(u => u.ProjectRoles).Select(u => new UserDetails
            {
                Id = u.Id,
                Name = u.Name,
                ProjectRoles = String.Join(", ", u.ProjectRoles.Select(r => r.Name)),
            });
        }
    }
}
