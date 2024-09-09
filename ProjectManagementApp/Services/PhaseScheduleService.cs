using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IPhaseScheduleService
    {
        IEnumerable<PhaseSchedule> GetSchedules();
        IEnumerable<GanttItem> GetGanttItems();
        Task CreateScheduleAsync(PhaseScheduleVm viewModel);
    }

    public class PhaseScheduleService(ApplicationDbContext dbContext) : IPhaseScheduleService
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<PhaseSchedule> GetSchedules() => dbContext.PhaseSchedules;

        /// <summary>
        /// Returns all schedules in the form of a gantt chart item.
        /// TODO: only check for items which would be displayed on the chart.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GanttItem> GetGanttItems()
        {
            foreach (var phase in dbContext.Phases)
            {
                foreach (var schedule in phase.Schedules)
                    yield return new GanttItem(schedule, phase.Name);
            }
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
