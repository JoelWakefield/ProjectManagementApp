using Microsoft.EntityFrameworkCore;

namespace ReactApp.Server.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Stage> Stages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.ProjectRoles)
                .WithMany(r => r.Users)
                .UsingEntity(
                    "ApplicationUserProjectRole",
                    l => l.HasOne(typeof(ProjectRole)).WithMany().HasForeignKey("ProjectRolesId").HasPrincipalKey(nameof(ProjectRole.Id)),
                    r => r.HasOne(typeof(AppUser)).WithMany().HasForeignKey("UsersId").HasPrincipalKey(nameof(AppUser.Id)),
                    j => j.HasKey("ProjectRolesId", "UsersId"));

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.OwnedProjects)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Phase>()
                .HasOne(ph => ph.Project)
                .WithMany(p => p.Phases)
                .HasForeignKey(ph => ph.ProjectId);

            modelBuilder.Entity<Phase>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.OwnedPhases)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Phase>()
                .HasOne(p => p.Stage)
                .WithMany(s => s.Phases)
                .HasForeignKey(p => p.StageId);
        }
    }
}
