using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IPhaseScheduleService
    {
        IEnumerable<PhaseSchedule> GetSchedules();
        Task<PhaseSchedule?> GetScheduleAsync(string phaseId);
        Task CreateScheduleAsync(PhaseSchedule schedule);
    }

    public class PhaseScheduleService(ApplicationDbContext dbContext) : IPhaseScheduleService
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<PhaseSchedule> GetSchedules() => dbContext.PhaseSchedules;

        public async Task<PhaseSchedule?> GetScheduleAsync(string phaseId)
        {
            return await dbContext.PhaseSchedules
                .OrderByDescending(s => s.CreatedOn)
                .FirstOrDefaultAsync(s => s.PhaseId == phaseId);
        }

        public async Task CreateScheduleAsync(PhaseSchedule schedule)
        {
            await dbContext.PhaseSchedules.AddAsync(schedule);
            await dbContext.SaveChangesAsync();
        }
    }
}
