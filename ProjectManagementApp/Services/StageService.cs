using AutoMapper;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IStageService
    {
        IEnumerable<StageVm> GetAllStages();
        Task CreateStageAsync(string name);
    }
    public class StageService(ApplicationDbContext dbContext, IMapper mapper) : IStageService
    {
        private ApplicationDbContext dbContext = dbContext;
        private IMapper mapper = mapper;

        public IEnumerable<StageVm> GetAllStages() => dbContext.Stages
            .OrderBy(s => s.OrderId)
            .Select(s => mapper.Map<StageVm>(s));

        public async Task CreateStageAsync(string name)
        {
            await dbContext.Stages.AddAsync(new Stage { Name = name });
            await dbContext.SaveChangesAsync();
        }
    }
}
