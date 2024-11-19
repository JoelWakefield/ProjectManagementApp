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
        public async Task<UserDetails> GetUser(string id)
        {
            var user = await dbContext.Users
                .Include(u => u.ProjectRoles)
                .SingleAsync(u => u.Id == id);

            return mapper.Map<AppUser, UserDetails>(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, UpdateUserRequest request)
        {
            try
            {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return NotFound(new { message = $"User with id ({id}) not found." });
                }

                //  update flat user data
                user.Name = request.Name;

                //  update projected user data
                foreach(KeyValuePair<string, bool> role in request.ProjectRoles)
                {
                    var projectRole = dbContext.ProjectRoles.Single(r => r.Name == role.Key);
                    if (projectRole == null)
                    {
                        return NotFound(new { message = $"Project Role ({role.Key}) not found." });
                    }

                    if (role.Value && !user.ProjectRoles.Any(r => r.Name == role.Key)) {
                        //  add role if requested and not currently listed
                        user.ProjectRoles.Add(projectRole);
                    } else if (!role.Value && user.ProjectRoles.Any(r => r.Name == role.Key)) {
                        //  remove role if requested and currently listed
                        user.ProjectRoles.Remove(projectRole);
                    }
                }

                await dbContext.SaveChangesAsync();
                return Ok(new { message = $" Todo Item  with id ({id}) successfully updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating user with id ({id})", error = ex.Message });
            }
        }
    }
}
