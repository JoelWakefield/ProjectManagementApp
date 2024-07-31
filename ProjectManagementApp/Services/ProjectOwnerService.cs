using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IProjectOwnerService
    {
        Task<ApplicationUser?> GetOwnerAsync(string projectId);

        Task AssignOwnerAsync(string projectId, string userId);
    }

    public class ProjectOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IProjectOwnerService
    {
        private ApplicationDbContext dbContext = dbContext;
        private UserManager<ApplicationUser> userManager = userManager;

        public async Task<ApplicationUser?> GetOwnerAsync(string projectId) {
            ProjectOwner? owner = await dbContext.ProjectOwners 
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefaultAsync(o => o.ProjectId == projectId);

            if (owner == null) { return null; }

            return await userManager.Users.FirstOrDefaultAsync(u => u.Id == owner!.UserId)!;
        }

        public async Task AssignOwnerAsync(string projectId, string userId)
        {
            await dbContext.ProjectOwners.AddAsync(new ProjectOwner()
            {
                ProjectId = projectId,
                UserId = userId,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
