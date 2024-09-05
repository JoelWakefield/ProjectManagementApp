using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;
using static MudBlazor.CategoryTypes;
using static System.Collections.Specialized.BitVector32;

namespace ProjectManagementApp.Services
{
	public interface IPhaseStageService
	{
		IEnumerable<KanBanSection> GetStageSections();
		IEnumerable<KanbanItem> GetKanbanItems();
		Task AssignStageAsync(string phaseId, string stageId);
	}

	public class PhaseStageService(ApplicationDbContext dbContext) : IPhaseStageService
	{
		private ApplicationDbContext dbContext = dbContext;

		public IEnumerable<KanBanSection> GetStageSections()
		{
			foreach (var stage in dbContext.Stages)
				yield return new KanBanSection(stage!.Name ?? "USERNAME NOT FOUND", true);
		}

		public IEnumerable<KanbanItem> GetKanbanItems()
		{
			// Assign each Phase to the owner's section
			foreach (var phase in dbContext.Phases)
				yield return new KanbanItem(phase.Name, phase.Id!, phase.Stage.Name!, phase.Stage.Id);
		}

		public async Task AssignStageAsync(string phaseId, string stageId)
		{
			Stage stage = await dbContext.Stages.FirstOrDefaultAsync(s => s.Id == stageId);
			Phase phase = await dbContext.Phases.FirstOrDefaultAsync(s => s.Id == phaseId);

			if (phase == null || stage == null)
			{
				return;
			}
			await dbContext.SaveChangesAsync();
		}
	}
}
