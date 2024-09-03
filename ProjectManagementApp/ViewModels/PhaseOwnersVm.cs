using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.ViewModels
{
	public class PhaseOwnersVm(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
	{
		private UserManager<ApplicationUser> userManager = userManager;
		private ApplicationDbContext dbContext = dbContext;

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
	}
}
