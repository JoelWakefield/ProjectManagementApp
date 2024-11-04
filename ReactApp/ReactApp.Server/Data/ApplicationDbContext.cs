using Microsoft.EntityFrameworkCore;

namespace ReactApp.Server.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }

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
        }
    }
}
