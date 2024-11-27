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
        public IEnumerable<UserVm> GetUsers()
        {
            return dbContext.Users
                .Include(u => u.ProjectRoles)
                .Select(u => mapper.Map<AppUser, UserVm>(u));
        }

        [HttpGet("{id}")]
        public async Task<EditUserVm> GetUser(string id)
        {
            var user = await dbContext.Users
                .Include(u => u.ProjectRoles)
                .SingleAsync(u => u.Id == id);

            var editUser = mapper.Map<AppUser, EditUserVm>(user);

            //  add non-listed roles as "false" to the edit user data
            var roles = dbContext.ProjectRoles;
            foreach(var role in roles)
            {
                if (!editUser.ProjectRoles.Any(r => r.Name == role.Name))
                {
                    editUser.ProjectRoles.Add(new EditUserProjectRole { Name = role.Name });
                }
            }

            return editUser;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, EditUserVm request)
        {
            try
            {
                var user = await dbContext.Users
                    .Include(u => u.ProjectRoles)
                    .SingleOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return NotFound(new { message = $"User with id ({id}) not found." });
                }

                //  update flat user data
                user.Name = request.Name;

                //  update projected user data
                foreach(ProjectRole projectRole in dbContext.ProjectRoles)
                {
                    var roleRequested = request.ProjectRoles.Any(r => r.Name == projectRole.Name);
                    var userHasRole = user.ProjectRoles.Any(r => r.Name == projectRole.Name);
                    if (roleRequested && !userHasRole)
                    {
                        //  add role if requested and user doesn't have it
                        user.ProjectRoles.Add(projectRole);
                    }
                    else if (!roleRequested && userHasRole)
                    {
                        //  remove role if not requested (only submits true values) and user has it
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
