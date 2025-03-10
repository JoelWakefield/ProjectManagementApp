using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Models;
using ReactApp.Server.ViewModels;

namespace ReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhaseController(ApplicationDbContext dbContext, IMapper mapper) : ControllerBase
    {
        private ApplicationDbContext dbContext = dbContext;
        private IMapper mapper = mapper;

        [HttpGet(Name = "GetPhases")]
        public IEnumerable<PhaseVm> GetAllPhases() {
            return mapper.Map<IEnumerable<Phase>, IEnumerable<PhaseVm>>(
                dbContext.Phases.Include(ph => ph.Project)
            );
        }

        [HttpPost(Name = "create")]
        public async Task<string> CreatePhaseAsync(CreatePhaseVm phaseVm)
        {
            try
            {
                Phase phase = new();

                //  set flat data
                phase.Name = phaseVm.Name;
                phase.Description = phaseVm.Description;
                phase.Priority = phaseVm.Priority;

                phase.Project = dbContext.Projects
                    .FirstOrDefault(p => p.Id == phaseVm.ProjectId)
                    ?? throw new Exception($"Cannot find project with id: {phaseVm.ProjectId}");
                phase.ProjectId = phaseVm.ProjectId;

                await dbContext.Phases.AddAsync(phase);
                await dbContext.SaveChangesAsync();

                return phase.Id;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpGet("details/{id}")]
        public async Task<PhaseVm> GetPhaseDetailsAsync(string id)
        {
            Phase phase = await dbContext.Phases
                .SingleAsync(p => p.Id == id);

            return mapper.Map<Phase, PhaseVm>(phase);
        }

        [HttpGet("edit/{id}")]
        public async Task<EditPhaseVm> GetPhaseEditAsync(string id)
        {
            Phase phase = await dbContext.Phases
                .SingleAsync(p => p.Id == id);

            return mapper.Map<Phase, EditPhaseVm>(phase);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdatePhaseAsync(EditPhaseVm phaseVm)
        {
            try
            {
                Phase phase = await dbContext.Phases
                    .SingleAsync(p => p.Id == phaseVm.Id);

                //  set flat data
                phase.Name = phaseVm.Name;
                phase.Description = phaseVm.Description;
                phase.Priority = phaseVm.Priority;

                await dbContext.SaveChangesAsync();
                return Ok(new { message = $"Phase ({phase.Id}) updated successfully."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error occured while updating phase ({phaseVm.Id}): {ex.ToString()}."
                });
            }
        }
        
        //  TODO: move to a project-phases API
        //public IEnumerable<PhaseVm> GetProjectPhases(string projectId);
    }
}
