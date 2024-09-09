using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<PhaseSchedule> PhaseSchedules { get; set; }

        public DbSet<ProjectUserRoleArchive> ProjectUserRoles { get; set; }
        public DbSet<ProjectOwnerArchive> ProjectOwners { get; set; }
        public DbSet<PhaseOwnerArchive> PhaseOwners { get; set; }
        public DbSet<PhaseStageArchive> PhaseStages { get; set; }
        public DbSet<PhaseAssignmentArchive> PhaseAssignments { get; set; }

        public DbSet<ProjectArchive> ProjectArchives { get; set; }
        public DbSet<ProjectRoleArchive> ProjectRoleArchives { get; set; }
        public DbSet<PhaseArchive> PhaseArchives { get; set; }
        public DbSet<StageArchive> StageArchives { get; set; }
        public DbSet<PhaseScheduleArchive> PhaseScheduleArchives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Phases)
                .WithOne(h => h.Project)
                .HasForeignKey(h => h.ProjectId)
                .HasPrincipalKey(p => p.Id)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnedProjects)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .HasPrincipalKey(o => o.Id)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnedPhases)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .HasPrincipalKey(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.AssignedPhases)
                .WithMany(p => p.Assignments);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.ProjectRoles)
                .WithMany(r => r.Users);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Schedules)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .HasPrincipalKey(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<Phase>()
                .HasMany(p => p.Schedules)
                .WithOne(p => p.Phase)
                .HasForeignKey(p => p.PhaseId)
                .HasPrincipalKey(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<Stage>()
                .HasMany(s => s.Phases)
                .WithOne(p => p.Stage)
                .HasForeignKey(p => p.StageId)
                .HasPrincipalKey(s => s.Id)
                .IsRequired();
        }
    }
}
