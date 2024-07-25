using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class ProjectRoleService
    {
        private ApplicationDbContext dbContext { get; set; }

        public ProjectRoleService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ProjectRole> GetUserProjectRoles(string userId)
        {
            var userRoleIds = DbContext.ProjectUserRoles.Where(r => r.UserId == UserId).Select(r => r.RoleId);

            DbContext.ProjectRoles.Where(r => userRoleIds.Contains(r.Id));
        }
    }
}
