using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
	public interface IPhaseAssignmentService
	{
		Task<Dictionary<string,PhaseAssignedRecord>> GetPhaseAssignedRecordsAsync();
		Task CreatePhaseAssignmentAsync(string phaseId, string userId, bool assigned);
	}

	public class PhaseAssignmentService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) : IPhaseAssignmentService
	{
        private ApplicationDbContext dbContext = dbContext;
		private UserManager<ApplicationUser> userManager = userManager;

		public async Task<Dictionary<string,PhaseAssignedRecord>> GetPhaseAssignedRecordsAsync()
		{
			Dictionary<string,PhaseAssignedRecord> records = new Dictionary<string, PhaseAssignedRecord>();

			IEnumerable<ApplicationUser> users = await userManager.Users.ToListAsync();
			IEnumerable<Phase> phases = dbContext.Phases;

			foreach (var phase in phases)
			{
				PhaseAssignedRecord record = new PhaseAssignedRecord(phase.Id,phase.Name);
				foreach (var user in users)
				{
					PhaseAssignment? assignment = await dbContext.PhaseAssignments
						.Where(p => p.PhaseId == phase.Id && p.UserId == user.Id)
						.OrderByDescending(p => p.CreatedOn)
						.FirstOrDefaultAsync();

					if (assignment == null)
						record.Assignments.Add(user.Id, false);
					else
						record.Assignments.Add(assignment.UserId, assignment.Assigned);
				}

				records.Add(phase.Id, record);
			}

			return records;
		}

		public async Task CreatePhaseAssignmentAsync(string phaseId, string userId, bool assigned)
		{
			await dbContext.PhaseAssignments.AddAsync(new PhaseAssignment()
			{
				PhaseId = phaseId,
				UserId = userId,
				Assigned = assigned
			});
			await dbContext.SaveChangesAsync();
		}
	}
}
