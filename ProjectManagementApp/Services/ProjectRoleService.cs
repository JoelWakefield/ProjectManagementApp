using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IProjectRoleService
    {
        IEnumerable<ProjectRole> GetAllRoles();
        Task<ProjectRole?> GetRoleAsync(string name);
        IEnumerable<ProjectRole>? GetUserRoles(string userId);
        IEnumerable<string> GetUserRoleNames(string userId);
        Task UpdateRoleForUserAsync(ApplicationUser user, ProjectRole role);
        Task<IEnumerable<ApplicationUser?>> GetUsersWithRoleAsync(string roleName);
        IEnumerable<UserWithRolesVm> GetAllUsersWithAllRoles();
        Task CreateRoleAsync(string name);
	}

    public class ProjectRoleService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) : IProjectRoleService
    {
        private ApplicationDbContext dbContext { get; set; } = dbContext;
        private UserManager<ApplicationUser> userManager { get; set; } = userManager;
        private IMapper mapper { get; set; } = mapper;

        public IEnumerable<ProjectRole> GetAllRoles() => dbContext.ProjectRoles;

        public async Task<ProjectRole?> GetRoleAsync(string name) => await dbContext.ProjectRoles.FirstOrDefaultAsync(r => r.Name == name);

        public IEnumerable<ProjectRole>? GetUserRoles(string userId)
        {
            var userRoles = dbContext.ProjectUserRoles.Where(r => r.UserId == userId).AsEnumerable();
            if (userRoles.Any() == false)
                return new List<ProjectRole>().AsEnumerable();

            var userRoleIds = userRoles.Select(r => r.RoleId);
            return dbContext.ProjectRoles.Where(r => userRoleIds.Contains(r.Id)).AsEnumerable();
        }

        public IEnumerable<string> GetUserRoleNames(string userId)
        {
            var roles = GetUserRoles(userId);
            if (roles?.Any() == false)
                return new List<string>().AsEnumerable();
            else
                return roles!.Select(r => r.Name).ToList();
        }

        public async Task UpdateRoleForUserAsync(ApplicationUser user, ProjectRole role)
        {
            if (user.ProjectRoles.Contains(role))
            {
                user.ProjectRoles.Remove(role);
            }
            else
            {
                user.ProjectRoles.Add(role);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser?>> GetUsersWithRoleAsync(string roleName)
        {
            ProjectRole? projectRole = await dbContext.ProjectRoles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (projectRole == null)
                return Enumerable.Empty<ApplicationUser>();
            else
                return projectRole.Users;
        }

        public async Task CreateRoleAsync(string name)
        {
            await dbContext.ProjectRoles.AddAsync(new ProjectRole() { Name = name });
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<UserWithRolesVm> GetAllUsersWithAllRoles()
        {
            IEnumerable<ApplicationUser> users = userManager.Users;

            foreach (var user in users)
            {
                yield return mapper.Map<UserWithRolesVm>(user);
            }
        }
    }
}
