using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public interface IStageService
    {
        IEnumerable<Stage> GetAllStages();
        Task CreateStageAsync(string name);
    }
    public class StageService(ApplicationDbContext dbContext) : IStageService
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<Stage> GetAllStages() => dbContext.Stages.OrderBy(s => s.OrderId);

        public async Task CreateStageAsync(string name)
        {
            await dbContext.Stages.AddAsync(new Stage { Name = name });
            await dbContext.SaveChangesAsync();
        }
    }
}
