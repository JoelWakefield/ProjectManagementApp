using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class ProjectOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        private ApplicationDbContext dbContext = dbContext;
        private UserManager<ApplicationUser> userManager = userManager;

        public ApplicationUser? GetOwner(string? projectId) {
            if (projectId == null) { return null; }

            ProjectOwner? owner = dbContext.ProjectOwners
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefault(o => o.ProjectId == projectId);

            if (owner == null) { return null; }

            return userManager.Users.FirstOrDefault(u => u.Id == owner!.UserId)!;
        }

        public async Task AssignOwnerAsync(string projectId, string userId)
        {
            dbContext.ProjectOwners.Add(new ProjectOwner()
            {
                ProjectId = projectId,
                UserId = userId,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
