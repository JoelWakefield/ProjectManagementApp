using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IProjectRoleService
    {
        IEnumerable<ProjectRole> GetAllRoles();
        Task<ProjectRole?> GetRoleAsync(string name);
        IEnumerable<ProjectRole>? GetUserRoles(string userId);
        IEnumerable<string> GetUserRoleNames(string userId);
        Task RemoveRoleFromUserAsync(string userId, string roleName);
        Task AddRoleToUserAsync(string userId, string roleName);
        Task<IEnumerable<ApplicationUser?>> GetUsersWithRoleAsync(string roleName);
        Task CreateRoleAsync(string name);
	}

	public class ProjectRoleService(ApplicationDbContext dbContext) : IProjectRoleService
    {
        private ApplicationDbContext dbContext { get; set; } = dbContext;

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

        public async Task RemoveRoleFromUserAsync(string userId, string roleName)
        {
            ProjectRole? projectRole = await dbContext.ProjectRoles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (projectRole == null) { return; }

            ProjectUserRole? projectUserRole = await dbContext.ProjectUserRoles.FirstOrDefaultAsync(r => r.RoleId == projectRole!.Id && r.UserId == userId);
            
            if (projectUserRole != null)
            {
                dbContext.ProjectUserRoles.Remove(projectUserRole);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddRoleToUserAsync(string userId, string roleName)
        {
            ProjectRole? projectRole = await dbContext.ProjectRoles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (projectRole == null) { return; }
            
            await dbContext.ProjectUserRoles.AddAsync(new ProjectUserRole { 
                UserId = userId, 
                RoleId = projectRole!.Id 
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser?>> GetUsersWithRoleAsync(string roleName)
        {
            ProjectRole? projectRole = await dbContext.ProjectRoles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (projectRole == null)
                return new List<ApplicationUser>().AsEnumerable();
            
            IEnumerable<string> userIds = dbContext.ProjectUserRoles
                .Where(r => r.RoleId == projectRole.Id)
                .Select(r => r.UserId)
                .AsEnumerable();

            return dbContext.Users.Where(u => userIds.Contains(u.Id));
        }

        public async Task CreateRoleAsync(string name)
        {
            await dbContext.ProjectRoles.AddAsync(new ProjectRole() { Name = name });
            await dbContext.SaveChangesAsync();
        }
    }
}
