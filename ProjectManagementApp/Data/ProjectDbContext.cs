using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.Data
{
    public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
    {
        public DbSet<Project>? Projects { get; set; }
    }
}
