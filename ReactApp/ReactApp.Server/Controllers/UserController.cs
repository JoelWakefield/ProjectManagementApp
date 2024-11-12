using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Data;
using ReactApp.Server.ViewModels;

namespace ReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(ApplicationDbContext dbContext, IMapper mapper) : ControllerBase
    {
        private ApplicationDbContext dbContext = dbContext;
        private IMapper mapper = mapper;

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<UserDetails> GetUsers()
        {
            return dbContext.Users
                .Include(u => u.ProjectRoles)
                .Select(u => mapper.Map<AppUser, UserDetails>(u));
        }

        [HttpGet("{id}")]
        public UserDetails GetUser(string id)
        {
            return mapper.Map<AppUser, UserDetails>(
                dbContext.Users.Single(u => u.Id == id)
            );
        }
    }
}
