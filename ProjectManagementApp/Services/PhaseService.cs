using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IPhaseService
    {
        IEnumerable<Phase> GetAllPhases();
        Task<Phase?> GetPhaseAsync(string id);
        Task CreatePhaseAsync(Phase phase);
        Task UpdatePhaseAsync(Phase phase);
        IEnumerable<Phase> GetProjectPhases(string projectId);
    }

    public class PhaseService(ApplicationDbContext dbContext) : IPhaseService
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<Phase> GetAllPhases() => dbContext.Phases;
        
        public async Task<Phase?> GetPhaseAsync(string id) => await dbContext.Phases.FirstOrDefaultAsync(p => p.Id == id);
        
        public async Task CreatePhaseAsync(Phase phase)
        {
            await dbContext.Phases.AddAsync(phase);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdatePhaseAsync(Phase phase)
        {
            Phase? existingPhase = await GetPhaseAsync(phase.Id);
            if (existingPhase != null)
            {
                existingPhase = phase;
                await dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Phase> GetProjectPhases(string projectId) => dbContext.Phases.Where(p => p.ProjectId == projectId);
    }
}
