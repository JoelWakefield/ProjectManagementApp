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


			modelBuilder.Entity<Phase>(entity =>
			{
				entity.HasIndex(e => e.OwnerId, "IX_Phases_OwnerId");
				entity.HasIndex(e => e.ProjectId, "IX_Phases_ProjectId");
				entity.HasIndex(e => e.StageId, "IX_Phases_StageId");

				entity.Property(e => e.Description).HasDefaultValueSql("''::text");
				entity.Property(e => e.OwnerId).HasDefaultValueSql("''::text");
				entity.Property(e => e.StageId).HasDefaultValueSql("''::text");

				entity.HasOne(d => d.Owner).WithMany(p => p.OwnedPhases).HasForeignKey(d => d.OwnerId);
				entity.HasOne(d => d.Project).WithMany(p => p.Phases).HasForeignKey(d => d.ProjectId);
				entity.HasOne(d => d.Stage).WithMany(p => p.Phases).HasForeignKey(d => d.StageId);

				entity.HasMany(d => d.Assignments).WithMany(p => p.AssignedPhases)
					.UsingEntity<Dictionary<string, object>>(
						"ApplicationUserPhase",
						r => r.HasOne<ApplicationUser>().WithMany().HasForeignKey("AssignmentsId"),
						l => l.HasOne<Phase>().WithMany().HasForeignKey("AssignedPhasesId"),
						j =>
						{
							j.HasKey("AssignedPhasesId", "AssignmentsId");
							j.ToTable("ApplicationUserPhase");
							j.HasIndex(new[] { "AssignmentsId" }, "IX_ApplicationUserPhase_AssignmentsId");
						});
			});

			modelBuilder.Entity<PhaseSchedule>(entity =>
			{
				entity.HasIndex(e => e.PhaseId, "IX_PhaseSchedules_PhaseId");
				entity.HasIndex(e => e.UserId, "IX_PhaseSchedules_UserId");

				entity.Property(e => e.UserId).HasDefaultValueSql("''::text");

				entity.HasOne(d => d.Phase).WithMany(p => p.Schedules).HasForeignKey(d => d.PhaseId);
				entity.HasOne(d => d.User).WithMany(p => p.Schedules).HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<Project>(entity =>
			{
				entity.HasIndex(e => e.OwnerId, "IX_Projects_OwnerId");

				entity.Property(e => e.CreatedBy).HasDefaultValueSql("''::text");
				entity.Property(e => e.Description).HasDefaultValueSql("''::text");
				entity.Property(e => e.Name).HasDefaultValueSql("''::text");
				entity.Property(e => e.OwnerId).HasDefaultValueSql("''::text");

				entity.HasOne(d => d.Owner).WithMany(p => p.OwnedProjects).HasForeignKey(d => d.OwnerId);
			});

			modelBuilder.Entity<ProjectRole>(entity =>
			{
				entity.HasMany(d => d.Users).WithMany(p => p.ProjectRoles)
					.UsingEntity<Dictionary<string, object>>(
						"ApplicationUserProjectRole",
						r => r.HasOne<ApplicationUser>().WithMany().HasForeignKey("UsersId"),
						l => l.HasOne<ProjectRole>().WithMany().HasForeignKey("ProjectRolesId"),
						j =>
						{
							j.HasKey("ProjectRolesId", "UsersId");
							j.ToTable("ApplicationUserProjectRole");
							j.HasIndex(new[] { "UsersId" }, "IX_ApplicationUserProjectRole_UsersId");
						});
			});

			modelBuilder.Entity<Stage>(entity =>
			{
				entity.Property(e => e.OrderId).HasDefaultValue(0);
			});


			//modelBuilder.Entity<ProjectUserRole>(entity =>
			//{
			//	entity.Property(e => e.Assigned).HasDefaultValue(false);
			//	entity.Property(e => e.CreatedOn).HasDefaultValueSql("'-infinity'::timestamp with time zone");
			//});


			//modelBuilder.Entity<Project>(entity =>
			//{
			//	entity.HasOne(d => d.Owner).WithMany(p => p.OwnedProjects).HasForeignKey(d => d.OwnerId);
			//});


			//         modelBuilder.Entity<Phase>(entity => {
			//	entity.HasOne(d => d.Owner).WithMany(p => p.OwnedPhases).HasForeignKey(d => d.OwnerId);
			//	entity.HasOne(d => d.Project).WithMany(p => p.Phases).HasForeignKey(d => d.ProjectId);
			//	entity.HasOne(d => d.Stage).WithMany(p => p.Phases).HasForeignKey(d => d.StageId);

			//	entity.HasMany(d => d.Assignments).WithMany(p => p.AssignedPhases)
			//		.UsingEntity<Dictionary<string, object>>(
			//			"ApplicationUserPhase",
			//			r => r.HasOne<ApplicationUser>().WithMany().HasForeignKey("AssignmentsId"),
			//			l => l.HasOne<Phase>().WithMany().HasForeignKey("AssignedPhasesId"),
			//			j =>
			//			{
			//				j.HasKey("AssignedPhasesId", "AssignmentsId");
			//				j.ToTable("ApplicationUserPhase");
			//				j.HasIndex(new[] { "AssignmentsId" }, "IX_ApplicationUserPhase_AssignmentsId");
			//			});
			//});

			//modelBuilder.Entity<PhaseSchedule>(entity =>
			//{
			//	entity.HasOne(d => d.Phase).WithMany(p => p.Schedules).HasForeignKey(d => d.PhaseId);
			//	entity.HasOne(d => d.User).WithMany(p => p.Schedules).HasForeignKey(d => d.UserId);
			//});

			//         modelBuilder.Entity<Project>()
			//             .HasMany(p => p.Phases)
			//             .WithOne(h => h.Project)
			//             .HasForeignKey(h => h.ProjectId)
			//             .HasPrincipalKey(p => p.Id);

			//modelBuilder.Entity<Project>(entity =>
			//{
			//	entity.HasOne(d => d.Owner).WithMany(p => p.OwnedProjects).HasForeignKey(d => d.OwnerId);
			//});

			//modelBuilder.Entity<ProjectRole>(entity =>
			//{
			//	entity.HasMany(d => d.Users).WithMany(p => p.ProjectRoles)
			//		.UsingEntity<Dictionary<string, object>>(
			//			"ApplicationUserProjectRole",
			//			r => r.HasOne<ApplicationUser>().WithMany().HasForeignKey("UsersId"),
			//			l => l.HasOne<ProjectRole>().WithMany().HasForeignKey("ProjectRolesId"),
			//			j =>
			//			{
			//				j.HasKey("ProjectRolesId", "UsersId");
			//				j.ToTable("ApplicationUserProjectRole");
			//				j.HasIndex(new[] { "UsersId" }, "IX_ApplicationUserProjectRole_UsersId");
			//			});
			//});

			//modelBuilder.Entity<ApplicationUser>()
			//             .HasMany(u => u.OwnedPhases)
			//             .WithOne(p => p.Owner)
			//             .HasForeignKey(p => p.OwnerId)
			//             .HasPrincipalKey(u => u.Id)
			//             .IsRequired();

			//         modelBuilder.Entity<ApplicationUser>()
			//             .HasMany(u => u.AssignedPhases)
			//             .WithMany(p => p.Assignments);

			//         modelBuilder.Entity<ApplicationUser>()
			//             .HasMany(u => u.Schedules)
			//             .WithOne(s => s.User)
			//             .HasForeignKey(s => s.UserId)
			//             .HasPrincipalKey(u => u.Id)
			//             .IsRequired();

			//modelBuilder.Entity<Stage>()
			//             .HasMany(s => s.Phases)
			//             .WithOne(p => p.Stage)
			//             .HasForeignKey(p => p.StageId)
			//             .HasPrincipalKey(s => s.Id)
			//             .IsRequired();
		}
    }
}
