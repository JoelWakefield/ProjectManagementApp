using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IPhaseService
    {
        IEnumerable<PhaseVm> GetAllPhases();
        Task<PhaseVm?> GetPhaseAsync(string id);
        Task CreatePhaseAsync(PhaseVm phase);
        Task UpdatePhaseAsync(PhaseVm phase);
    }

    public class PhaseService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IMapper mapper) : IPhaseService
    {
        private UserManager<ApplicationUser> userManager { get; set; } = userManager;
        private ApplicationDbContext dbContext = dbContext;
		private IMapper mapper = mapper;

		public IEnumerable<PhaseVm> GetAllPhases() => dbContext.Phases.Select(mapper.Map<PhaseVm>);

        public async Task<PhaseVm?> GetPhaseAsync(string id) => mapper.Map<PhaseVm>(
            await dbContext.Phases.FirstOrDefaultAsync(p => p.Id == id));

        public async Task CreatePhaseAsync(PhaseVm phaseVm)
        {
            Phase phase = mapper.Map<Phase>(phaseVm);
            phase.Owner = await userManager.Users.FirstOrDefaultAsync(u => u.Id == phase.OwnerId);
            phase.Project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == phase.ProjectId);
            phase.Stage = await dbContext.Stages.OrderBy(p => p.OrderId).FirstOrDefaultAsync();

            await dbContext.Phases.AddAsync(phase);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdatePhaseAsync(PhaseVm phase)
        {
            Phase? existingPhase = await dbContext.Phases.FirstOrDefaultAsync(p => p.Id == phase.Id);
            if (existingPhase != null)
            {
                //  update only edited fields
                existingPhase.Name = phase.Name;
                existingPhase.Description = phase.Description;
                existingPhase.Priority = phase.Priority;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
