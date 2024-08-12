using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
	public interface IPhaseOwnerService
	{
		Task<IEnumerable<KanbanItem>> GetPhaseOwnerKanbanItemsAsync();
		Task<ApplicationUser?> GetOwnerAsync(string phaseId);
		Task AssignOwnerAsync(string phaseId, string userId);
	}

	public class PhaseOwnerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IPhaseOwnerService
	{
		private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager = userManager;

		public async Task<IEnumerable<KanbanItem>> GetPhaseOwnerKanbanItemsAsync()
		{
			List<KanbanItem> items = new List<KanbanItem>();

			// Assign each phase to the owner's section
			foreach (var phase in dbContext.Phases.ToList())
			{
				ApplicationUser? owner = await GetOwnerAsync(phase.Id);

				if (owner == null)
					items.Add(new KanbanItem(phase.Name, phase.Id!, "UNOWNED", String.Empty));
				else
					items.Add(new KanbanItem(phase.Name, phase.Id!, owner.UserName!, owner.Id));
			}

			return items.AsEnumerable();
		} 

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
