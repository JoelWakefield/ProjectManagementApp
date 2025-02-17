using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.Data
{
    //public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectOwner> ProjectOwners { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectUserRole> ProjectUserRoles { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<PhaseOwner> PhaseOwners { get; set; }
        public DbSet<PhaseStage> PhaseStages { get; set; }
        public DbSet<PhaseAssignment> PhaseAssignments { get; set; }
        public DbSet<PhaseSchedule> PhaseSchedules { get; set; }
    }
}
