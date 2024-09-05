using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
	public interface IPhaseOwnerService
	{
        IEnumerable<KanBanSection> GetOwnerSections();
        IEnumerable<KanbanItem> GetKanbanItems();
		Task AssignOwnerAsync(string phaseId, string userId);
	}

	public class PhaseOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IPhaseOwnerService
	{
		private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager = userManager;

        public IEnumerable<KanBanSection> GetOwnerSections()
        {
            foreach (var owner in userManager.Users)
                yield return new KanBanSection(owner.UserName ?? "USERNAME NOT FOUND", true);
        }

        /// <summary>
        /// Returns the phases in the form of KanbanItems.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KanbanItem> GetKanbanItems()
        {
            // Assign each phase to the owner's section
            foreach (var phase in dbContext.Phases)
                yield return new KanbanItem(phase.Name, phase.Id, phase.Owner.UserName, phase.Owner.Id);
        }

        public async Task AssignOwnerAsync(string phaseId, string userId)
		{
			ApplicationUser? user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null) { return; }

			Phase? phase = await dbContext.Phases.FirstOrDefaultAsync(p => p.Id == phaseId);
			if (phase == null) { return; }
			
			phase.Owner = user;
			await dbContext.SaveChangesAsync();
		}
    }
}
