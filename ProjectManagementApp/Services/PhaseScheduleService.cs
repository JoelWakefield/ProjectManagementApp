using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IPhaseScheduleService
    {
        IEnumerable<PhaseSchedule> GetSchedules();
        Task<PhaseSchedule?> GetSchedule(string phaseId);
    }

    public class PhaseScheduleService(ApplicationDbContext dbContext) : IPhaseScheduleService
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<PhaseSchedule> GetSchedules() => dbContext.PhaseSchedules;

        public async Task<PhaseSchedule?> GetSchedule(string phaseId)
        {
            return await dbContext.PhaseSchedules
                .OrderByDescending(s => s.CreatedOn)
                .FirstOrDefaultAsync(s => s.PhaseId == phaseId);
        }
    }
}
