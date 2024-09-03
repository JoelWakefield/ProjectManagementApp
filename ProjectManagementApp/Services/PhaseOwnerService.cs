using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
	public interface IPhaseOwnerService
	{
		Task AssignOwnerAsync(string phaseId, string userId);
		Task AssignOwnerAsync(Phase phase, string userId);
	}

	public class PhaseOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IPhaseOwnerService
	{
		private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager = userManager;

		public async Task AssignOwnerAsync(string phaseId, string userId)
		{
			ApplicationUser? user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null) { return; }

			Phase? phase = await dbContext.Phases.FirstOrDefaultAsync(p => p.Id == phaseId);
			if (phase == null) { return; }
			
			phase.Owner = user;
			await dbContext.SaveChangesAsync();
		}

        public async Task AssignOwnerAsync(Phase phase, string userId)
        {
            ApplicationUser? user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) { return; }

            phase.Owner = user;
            await dbContext.SaveChangesAsync();
        }
    }
}
