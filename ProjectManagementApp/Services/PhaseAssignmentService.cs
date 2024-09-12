using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
	public interface IPhaseAssignmentService
	{
		Task<Dictionary<string,PhaseAssignedRecord>> GetPhaseAssignedRecordsAsync();
		Task TogglePhaseAssignmentAsync(string phaseId, string userId, bool assigned);
	}

	public class PhaseAssignmentService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IPhaseAssignmentService
	{
        private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager = userManager;

		public async Task<Dictionary<string,PhaseAssignedRecord>> GetPhaseAssignedRecordsAsync()
		{
			Dictionary<string,PhaseAssignedRecord> records = new Dictionary<string, PhaseAssignedRecord>();

			IEnumerable<ApplicationUser> users = await userManager.Users.ToListAsync();
			IEnumerable<Phase> phases = await dbContext.Phases.ToListAsync();

			foreach (var phase in phases)
			{
				PhaseAssignedRecord record = new PhaseAssignedRecord()
				{
					PhaseId		= phase.Id,
					Name		= phase.Name,
					Assignments = users.ToDictionary(
						u => u.Id,
						phase.Assignments.Contains
					)
				};

				records.Add(phase.Id, record);
			}

			return records;
		}

		public async Task TogglePhaseAssignmentAsync(string phaseId, string userId, bool isAssigned)
		{
			Phase? phase = await dbContext.Phases.Include(p => p.Assignments).FirstOrDefaultAsync(p => p.Id == phaseId);
			ApplicationUser? user = await userManager.Users.FirstOrDefaultAsync(p => p.Id == userId);

			if (user == null || phase == null)
			{
				return;
			}

			if (isAssigned)
			{
				phase.Assignments.Remove(user);
			}
			else
			{
				phase.Assignments.Add(user);
			}

			await dbContext.SaveChangesAsync();
		}
	}
}
