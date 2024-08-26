using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IPhaseScheduleService
    {
        IEnumerable<PhaseSchedule> GetSchedules();
        Task<PhaseSchedule?> GetScheduleAsync(string phaseId);
        Task<IEnumerable<GanttItem>> GetGanttItems();
        Task CreateScheduleAsync(PhaseScheduleVm viewModel);
    }

    public class PhaseScheduleService(ApplicationDbContext dbContext, IPhaseService phaseService) : IPhaseScheduleService
    {
        private ApplicationDbContext dbContext = dbContext;
        private IPhaseService phaseService = phaseService;

        public IEnumerable<PhaseSchedule> GetSchedules() => dbContext.PhaseSchedules;

        /// <summary>
        /// Returns all schedules in the form of a gantt chart item.
        /// TODO: only check for items which would be displayed on the chart.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GanttItem>> GetGanttItems()
        {
            var phases = phaseService.GetAllPhases().ToList();
            var items = new List<GanttItem>();

            foreach (var phase in phases)
            {
                var schedule = await GetScheduleAsync(phase.Id);

                if (schedule != null)
                    items.Add(new GanttItem(schedule, phase.Name));
            }

            return items;
        }

        public async Task<PhaseSchedule?> GetScheduleAsync(string phaseId)
        {
            return await dbContext.PhaseSchedules
                .OrderByDescending(s => s.CreatedOn)
                .FirstOrDefaultAsync(s => s.PhaseId == phaseId);
        }

        public async Task CreateScheduleAsync(PhaseScheduleVm viewModel)
        {
            PhaseSchedule schedule = new PhaseSchedule()
            {
                PhaseId = viewModel.PhaseId,
                Start = viewModel.Start.ToDateTime(TimeOnly.MinValue),
                End = viewModel.End.ToDateTime(TimeOnly.MinValue),
            };
            await dbContext.PhaseSchedules.AddAsync(schedule);
            await dbContext.SaveChangesAsync();
        }
    }
}
