using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class ProjectOwnerService
    {
        private ApplicationDbContext dbContext;
        private UserManager<ApplicationUser> userManager;

        public ProjectOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public ApplicationUser? GetOwner(string? projectId) {
            if (projectId == null) { return null; }

            ProjectOwner? owner = dbContext.ProjectOwners
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefault(o => o.ProjectId == projectId);

            if (owner == null) { return null; }

            return userManager.Users.FirstOrDefault(u => u.Id == owner!.UserId)!;
        }
    }
}
