using Microsoft.AspNetCore.Mvc;
using ReactApp.Server.Data;

namespace ReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(ApplicationDbContext dbContext) : ControllerBase
    {
        private ApplicationDbContext dbContext = dbContext;

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<AppUser> GetUsers()
        {
            return dbContext.Users;
        }
    }
}
