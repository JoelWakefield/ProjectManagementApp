using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
	public interface IPhaseStageService
	{
		Task<Stage?> GetStageAsync(string phaseId);

		Task AssignStageAsync(string phaseId, string stageId);
	}

	public class PhaseStageService(ApplicationDbContext dbContext) : IPhaseStageService
	{
		private ApplicationDbContext dbContext = dbContext;

		public async Task<Stage?> GetStageAsync(string phaseId)
		{
			PhaseStage? phaseStage = await dbContext.PhaseStages
				.OrderByDescending(o => o.CreatedOn)
				.FirstOrDefaultAsync(o => o.PhaseId == phaseId);

			if (phaseStage == null) { return null; }

			return await dbContext.Stages.FirstOrDefaultAsync(s => s.Id == phaseStage.StageId);
		}

		public async Task AssignStageAsync(string phaseId, string stageId)
		{
			await dbContext.PhaseStages.AddAsync(new PhaseStage()
			{
				PhaseId = phaseId,
				StageId = stageId,
			});
			await dbContext.SaveChangesAsync();
		}
	}
}
