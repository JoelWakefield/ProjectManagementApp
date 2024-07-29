using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class ProjectRoleService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        private ApplicationDbContext dbContext { get; set; } = dbContext;
        private UserManager<ApplicationUser> userManager { get; set; } = userManager;

        public IEnumerable<ProjectRole> GetAllRoles() => dbContext.ProjectRoles;

        public ProjectRole? GetRole(string name) => dbContext.ProjectRoles.FirstOrDefault(r => r.Name == name);

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

        public void RemoveRoleFromUser(string userId, string roleName)
        {
            ProjectRole? projectRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == roleName);

            if (projectRole == null) { return; }

            ProjectUserRole? projectUserRole = dbContext.ProjectUserRoles.FirstOrDefault(r => r.RoleId == projectRole!.Id && r.UserId == userId);
            
            if (projectUserRole != null)
            {
                dbContext.ProjectUserRoles.Remove(projectUserRole);
                dbContext.SaveChanges();
            }
        }

        public void AddRoleToUser(string userId, string roleName)
        {
            ProjectRole? projectRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == roleName);
            if (projectRole == null) { return; }
            
            dbContext.ProjectUserRoles.Add(new ProjectUserRole { 
                UserId = userId, 
                RoleId = projectRole!.Id 
            });

            dbContext.SaveChanges();
        }

        public IEnumerable<ApplicationUser>? GetUsersWithRole(string roleName)
        {
            ProjectRole? projectRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == roleName);
            if (projectRole == null)
                return new List<ApplicationUser>().AsEnumerable();
            
            IEnumerable<string> userIds = dbContext.ProjectUserRoles
                .Where(r => r.RoleId == projectRole.Id)
                .Select(r => r.UserId)
                .AsEnumerable();

            return userManager.Users.Where(u => userIds.Contains(u.Id));
        }

        public async Task CreateRoleAsync(string name)
        {
            dbContext.ProjectRoles.Add(new ProjectRole() { Name = name });
            await dbContext.SaveChangesAsync();
        }
    }
}
