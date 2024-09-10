using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IPhaseService
    {
        IEnumerable<PhaseVm> GetAllPhases();
        Task<PhaseVm?> GetPhaseAsync(string id);
        Task<string> CreatePhaseAsync(CreatePhaseVm phase);
        Task UpdatePhaseAsync(PhaseVm phase);
    }

    public class PhaseService(ApplicationDbContext dbContext, IMapper mapper) : IPhaseService
    {
        private readonly ApplicationDbContext dbContext = dbContext;
		private readonly IMapper mapper = mapper;

		public IEnumerable<PhaseVm> GetAllPhases() => mapper.Map<IEnumerable<Phase>,IEnumerable<PhaseVm>>(dbContext.Phases);

        public async Task<PhaseVm?> GetPhaseAsync(string id) => mapper.Map<PhaseVm>(
            await dbContext.Phases.FirstOrDefaultAsync(p => p.Id == id));

        public async Task<string> CreatePhaseAsync(CreatePhaseVm phaseVm)
        {
            Phase phase = mapper.Map<Phase>(phaseVm);

            await dbContext.Phases.AddAsync(phase);
            await dbContext.SaveChangesAsync();

            return phase.Id;
        }

        public async Task UpdatePhaseAsync(PhaseVm phaseVm)
        {
            Phase phase = mapper.Map<Phase>(phaseVm);
            dbContext.Phases.Attach(phase);
            dbContext.Entry(phase).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
