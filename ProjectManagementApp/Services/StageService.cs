using ProjectManagementApp.Data;

namespace ProjectManagementApp.Services
{
    public class StageService(ApplicationDbContext dbContext)
    {
        private ApplicationDbContext dbContext = dbContext;

        public IEnumerable<Stage> GetAllStages() => dbContext.Stages;

        public async Task CreateStageAsync(string name)
        {
            await dbContext.Stages.AddAsync(new Stage { Name = name });
            await dbContext.SaveChangesAsync();
        }
    }
}
