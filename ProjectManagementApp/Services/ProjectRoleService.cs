using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Services
{
    public class ProjectRoleService
    {
        private ApplicationDbContext dbContext { get; set; }

        public ProjectRoleService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

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
    }
}
