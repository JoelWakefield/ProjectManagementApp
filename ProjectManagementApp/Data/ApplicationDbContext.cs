using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ProjectManagementApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Phases)
                .WithOne(h => h.Project)
                .HasForeignKey(h => h.ProjectId)
                .HasPrincipalKey(p => p.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnedProjects)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .HasPrincipalKey(o => o.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnedPhases)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .HasPrincipalKey(u => u.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.AssignedPhases)
                .WithMany(p => p.PhaseAssignments);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.ProjectRoles)
                .WithMany(r => r.Users);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Schedules)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .HasPrincipalKey(u => u.Id);

            modelBuilder.Entity<PhaseSchedule>()
                .HasOne(s => s.Phase)
                .WithOne(p => p.PhaseSchedule)
                .HasForeignKey<Phase>(p => p.PhaseScheduleId)
                .HasPrincipalKey<PhaseSchedule>(p => p.Id);

            modelBuilder.Entity<Stage>()
                .HasMany(s => s.Phases)
                .WithOne(p => p.Stage)
                .HasForeignKey(p => p.StageId)
                .HasPrincipalKey(s => s.Id);
        }
    }
}
