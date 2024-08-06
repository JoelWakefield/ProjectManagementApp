using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
	public interface IPhaseOwnerService
	{
		Task<ApplicationUser?> GetOwnerAsync(string phaseId);

		Task AssignOwnerAsync(string phaseId, string userId);
	}

	public class PhaseOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IPhaseOwnerService
	{
		private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager = userManager;

		public async Task<ApplicationUser?> GetOwnerAsync(string phaseId)
		{
			PhaseOwner? owner = await dbContext.PhaseOwners
				.OrderByDescending(o => o.CreatedOn)
				.FirstOrDefaultAsync(o => o.PhaseId == phaseId);

			if (owner == null) { return null; }

			return await userManager.Users.FirstOrDefaultAsync(u => u.Id == owner!.UserId)!;
		}

		public async Task AssignOwnerAsync(string phaseId, string userId)
		{
			await dbContext.PhaseOwners.AddAsync(new PhaseOwner()
			{
				PhaseId = phaseId,
				UserId = userId,
			});
			await dbContext.SaveChangesAsync();
		}
	}
}
