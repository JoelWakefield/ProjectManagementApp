using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class PhaseService(ApplicationDbContext dbContext)
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<Phase> GetAllPhases() => dbContext.Phases;
        public async Task<Phase?> GetPhaseAsync(string id) => await dbContext.Phases.FirstOrDefaultAsync(p => p.Id == id);
        public async Task UpdatePhaseAsync(Phase phase)
        {
            Phase? existingPhase = await GetPhaseAsync(phase.Id);
            if (existingPhase != null)
            {
                existingPhase = phase;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
