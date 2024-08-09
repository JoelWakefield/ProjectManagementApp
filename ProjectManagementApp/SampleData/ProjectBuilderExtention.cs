using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class ProjectBuilderExtention
    {
        public static async Task BuildFakeProject(this WebApplication app)
        {
            //  Setup services
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //  Create initial roles (for identity and project)
            if (roleManager.RoleExistsAsync(IdentityRoles.Admin).Result == false)
                await roleManager.CreateAsync(new IdentityRole(IdentityRoles.Admin));
            if (roleManager.RoleExistsAsync(IdentityRoles.User).Result == false)
                await roleManager.CreateAsync(new IdentityRole(IdentityRoles.User));

            if (dbContext.ProjectRoles.Any(r => r.Name == ProjectRoles.Manager) == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = ProjectRoles.Manager });
            if (dbContext.ProjectRoles.Any(r => r.Name == ProjectRoles.Owner) == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = ProjectRoles.Owner });
            if (dbContext.ProjectRoles.Any(r => r.Name == ProjectRoles.Member) == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = ProjectRoles.Member });

            //  Create initial users
            if (userManager.Users.Any(u => u.UserName == Users.Bert.UserName) == false)
                await userManager.CreateAsync(Users.Bert);
            if (userManager.Users.Any(u => u.UserName == Users.Mylo.UserName) == false)
                await userManager.CreateAsync(Users.Mylo);
            if (userManager.Users.Any(u => u.UserName == Users.Alayah.UserName) == false)
                await userManager.CreateAsync(Users.Alayah);
            if (userManager.Users.Any(u => u.UserName == Users.Zahir.UserName) == false)
                await userManager.CreateAsync(Users.Zahir);

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Pull the updated/pre-existing user data
            Users.Bert = userManager.Users.FirstOrDefault(u => u.UserName == Users.Bert.UserName)!;
            Users.Mylo = userManager.Users.FirstOrDefault(u => u.UserName == Users.Mylo.UserName)!;
            Users.Alayah = userManager.Users.FirstOrDefault(u => u.UserName == Users.Alayah.UserName)!;
            Users.Zahir = userManager.Users.FirstOrDefault(u => u.UserName == Users.Zahir.UserName)!;

            //  Pull the updated/pre-existing project role data
            ProjectRole managerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Manager)!;
            ProjectRole ownerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Owner)!;
            ProjectRole memberRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Member)!;


            //  Assign identity roles to new users
            if ((await userManager.GetRolesAsync(Users.Mylo)).Contains(IdentityRoles.Admin) == false)
                await userManager.AddToRoleAsync(Users.Mylo, IdentityRoles.Admin);

            if ((await userManager.GetRolesAsync(Users.Mylo)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(Users.Mylo, IdentityRoles.User);
            if ((await userManager.GetRolesAsync(Users.Bert)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(Users.Bert, IdentityRoles.User);
            if ((await userManager.GetRolesAsync(Users.Alayah)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(Users.Alayah, IdentityRoles.User);
            if ((await userManager.GetRolesAsync(Users.Zahir)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(Users.Zahir, IdentityRoles.User);

            //  Assign project roles to new users
            if (dbContext.ProjectUserRoles.Any(r => r.UserId == Users.Mylo.Id && r.RoleId == managerRole.Id) == false)
                dbContext.ProjectUserRoles.Add(new ProjectUserRole() { UserId = Users.Mylo.Id, RoleId = managerRole.Id });

            if (dbContext.ProjectUserRoles.Any(r => r.UserId == Users.Mylo.Id && r.RoleId == ownerRole.Id) == false)
                dbContext.ProjectUserRoles.Add(new ProjectUserRole() { UserId = Users.Mylo.Id, RoleId = ownerRole.Id });
            if (dbContext.ProjectUserRoles.Any(r => r.UserId == Users.Alayah.Id && r.RoleId == ownerRole.Id) == false)
                dbContext.ProjectUserRoles.Add(new ProjectUserRole() { UserId = Users.Alayah.Id, RoleId = ownerRole.Id });

            if (dbContext.ProjectUserRoles.Any(r => r.UserId == Users.Mylo.Id && r.RoleId == memberRole.Id) == false)
                dbContext.ProjectUserRoles.Add(new ProjectUserRole() { UserId = Users.Mylo.Id, RoleId = memberRole.Id });
            if (dbContext.ProjectUserRoles.Any(r => r.UserId == Users.Bert.Id && r.RoleId == memberRole.Id) == false)
                dbContext.ProjectUserRoles.Add(new ProjectUserRole() { UserId = Users.Bert.Id, RoleId = memberRole.Id });
            if (dbContext.ProjectUserRoles.Any(r => r.UserId == Users.Alayah.Id && r.RoleId == memberRole.Id) == false)
                dbContext.ProjectUserRoles.Add(new ProjectUserRole() { UserId = Users.Alayah.Id, RoleId = memberRole.Id });
            if (dbContext.ProjectUserRoles.Any(r => r.UserId == Users.Zahir.Id && r.RoleId == memberRole.Id) == false)
                dbContext.ProjectUserRoles.Add(new ProjectUserRole() { UserId = Users.Zahir.Id, RoleId = memberRole.Id });

            //  Setup initial projects
            if (dbContext.Projects.Any(p => p.Name == Projects.SimpleProject.Name) == false)
                dbContext.Projects.Add(Projects.SimpleProject);
            if (dbContext.Projects.Any(p => p.Name == Projects.MultiPhaseProject.Name) == false)
                dbContext.Projects.Add(Projects.MultiPhaseProject);
            if (dbContext.Projects.Any(p => p.Name == Projects.MultiPersonProject.Name) == false)
                dbContext.Projects.Add(Projects.MultiPersonProject);
            if (dbContext.Projects.Any(p => p.Name == Projects.MultiPersonMultiPhaseProject.Name) == false)
                dbContext.Projects.Add(Projects.MultiPersonMultiPhaseProject);

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Pull the updated/pre-existing project info
            Projects.SimpleProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.SimpleProject.Name)!;
            Projects.MultiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPhaseProject.Name)!;
            Projects.MultiPersonProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPersonProject.Name)!;
            Projects.MultiPersonMultiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPersonMultiPhaseProject.Name)!;

            //  Setup owners with projects (if a project exists, then it already has an owner - no need to add another record)
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == Projects.SimpleProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = Projects.SimpleProject.Id,
                    UserId = Users.Mylo.Id,
                });
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == Projects.MultiPhaseProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = Projects.MultiPhaseProject.Id,
                    UserId = Users.Mylo.Id,
                });
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == Projects.MultiPersonProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = Projects.MultiPersonProject.Id,
                    UserId = Users.Alayah.Id,
                });
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == Projects.MultiPersonMultiPhaseProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = Projects.MultiPersonMultiPhaseProject.Id,
                    UserId = Users.Alayah.Id,
                });

            //  Add Stages
            if (dbContext.Stages.Any(s => s.Name == Stages.Backlog.Name) == false)
                dbContext.Stages.Add(Stages.Backlog);
            if (dbContext.Stages.Any(s => s.Name == Stages.ToDo.Name) == false)
                dbContext.Stages.Add(Stages.ToDo);
            if (dbContext.Stages.Any(s => s.Name == Stages.InProgress.Name) == false)
                dbContext.Stages.Add(Stages.InProgress);
            if (dbContext.Stages.Any(s => s.Name == Stages.Review.Name) == false)
                dbContext.Stages.Add(Stages.Review);
            if (dbContext.Stages.Any(s => s.Name == Stages.Complete.Name) == false)
                dbContext.Stages.Add(Stages.Complete);
            if (dbContext.Stages.Any(s => s.Name == Stages.Canceled.Name) == false)
                dbContext.Stages.Add(Stages.Canceled);

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Pull stage data
            Stages.Backlog = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Backlog.Name)!;
            Stages.ToDo = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.ToDo.Name)!;
            Stages.InProgress = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.InProgress.Name)!;
            Stages.Review = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Review.Name)!;
            Stages.Complete = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Complete.Name)!;
            Stages.Canceled = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Canceled.Name)!;

            //  Add Phases
            if (dbContext.Phases.Any(p => p.ProjectId == Projects.SimpleProject.Id && p.Name == Phases.SimplePlanning.Name) == false)
                dbContext.Phases.Add(Phases.SimplePlanning);
            if (dbContext.Phases.Any(p => p.ProjectId == Projects.SimpleProject.Id && p.Name == Phases.SimpleSetup.Name) == false)
                dbContext.Phases.Add(Phases.SimpleSetup);
            if (dbContext.Phases.Any(p => p.ProjectId == Projects.SimpleProject.Id && p.Name == Phases.SimpleDataEntry.Name) == false)
                dbContext.Phases.Add(Phases.SimpleDataEntry);
            if (dbContext.Phases.Any(p => p.ProjectId == Projects.SimpleProject.Id && p.Name == Phases.SimpleQA.Name) == false)
                dbContext.Phases.Add(Phases.SimpleQA);
            if (dbContext.Phases.Any(p => p.ProjectId == Projects.SimpleProject.Id && p.Name == Phases.SimpleNotifyCompletion.Name) == false)
                dbContext.Phases.Add(Phases.SimpleNotifyCompletion);
            if (dbContext.Phases.Any(p => p.ProjectId == Projects.SimpleProject.Id && p.Name == Phases.SimplePostAnalytics.Name) == false)
                dbContext.Phases.Add(Phases.SimplePostAnalytics);

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Pull most up to date phase data
            Phases.SimplePlanning = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimplePlanning.Name)!;
            Phases.SimpleSetup = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleSetup.Name)!;
            Phases.SimpleDataEntry = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleDataEntry.Name)!;
            Phases.SimpleQA = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleQA.Name)!;
            Phases.SimpleNotifyCompletion = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleNotifyCompletion.Name)!;
            Phases.SimplePostAnalytics = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimplePostAnalytics.Name)!;

            //  Add phase owners
            if (dbContext.PhaseOwners.Any(p => p.PhaseId == Phases.SimplePlanning.Id) == false)
                dbContext.PhaseOwners.Add(new PhaseOwner { PhaseId = Phases.SimplePlanning.Id, UserId = Users.Bert.Id });
            if (dbContext.PhaseOwners.Any(p => p.PhaseId == Phases.SimpleSetup.Id) == false)
                dbContext.PhaseOwners.Add(new PhaseOwner { PhaseId = Phases.SimpleSetup.Id, UserId = Users.Zahir.Id });
            if (dbContext.PhaseOwners.Any(p => p.PhaseId == Phases.SimpleDataEntry.Id) == false)
                dbContext.PhaseOwners.Add(new PhaseOwner { PhaseId = Phases.SimpleDataEntry.Id, UserId = Users.Alayah.Id });
            if (dbContext.PhaseOwners.Any(p => p.PhaseId == Phases.SimpleQA.Id) == false)
                dbContext.PhaseOwners.Add(new PhaseOwner { PhaseId = Phases.SimpleQA.Id, UserId = Users.Bert.Id });
            if (dbContext.PhaseOwners.Any(p => p.PhaseId == Phases.SimpleNotifyCompletion.Id) == false)
                dbContext.PhaseOwners.Add(new PhaseOwner { PhaseId = Phases.SimpleNotifyCompletion.Id, UserId = Users.Alayah.Id });
            if (dbContext.PhaseOwners.Any(p => p.PhaseId == Phases.SimplePostAnalytics.Id) == false)
                dbContext.PhaseOwners.Add(new PhaseOwner { PhaseId = Phases.SimplePostAnalytics.Id, UserId = Users.Zahir.Id });

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Add phase stages
            if (dbContext.PhaseStages.Any(p => p.PhaseId == Phases.SimplePlanning.Id && p.StageId == Stages.Complete.Id) == false)
                dbContext.PhaseStages.Add(new PhaseStage { PhaseId = Phases.SimplePlanning.Id, StageId = Stages.Complete.Id });
            if (dbContext.PhaseStages.Any(p => p.PhaseId == Phases.SimpleSetup.Id && p.StageId == Stages.Review.Id) == false)
                dbContext.PhaseStages.Add(new PhaseStage { PhaseId = Phases.SimpleSetup.Id, StageId = Stages.Review.Id });
            if (dbContext.PhaseStages.Any(p => p.PhaseId == Phases.SimpleDataEntry.Id && p.StageId == Stages.InProgress.Id) == false)
                dbContext.PhaseStages.Add(new PhaseStage { PhaseId = Phases.SimpleDataEntry.Id, StageId = Stages.InProgress.Id });
            if (dbContext.PhaseStages.Any(p => p.PhaseId == Phases.SimpleQA.Id && p.StageId == Stages.ToDo.Id) == false)
                dbContext.PhaseStages.Add(new PhaseStage { PhaseId = Phases.SimpleQA.Id, StageId = Stages.ToDo.Id });
            if (dbContext.PhaseStages.Any(p => p.PhaseId == Phases.SimpleNotifyCompletion.Id && p.StageId == Stages.Backlog.Id) == false)
                dbContext.PhaseStages.Add(new PhaseStage { PhaseId = Phases.SimpleNotifyCompletion.Id, StageId = Stages.Backlog.Id });
            if (dbContext.PhaseStages.Any(p => p.PhaseId == Phases.SimplePostAnalytics.Id && p.StageId == Stages.Canceled.Id) == false)
                dbContext.PhaseStages.Add(new PhaseStage { PhaseId = Phases.SimplePostAnalytics.Id, StageId = Stages.Canceled.Id });

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Assign users to phases
            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimplePlanning.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimplePlanning.Id, UserId = Users.Alayah.Id, Assigned = true });
            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimplePlanning.Id && p.UserId == Users.Bert.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimplePlanning.Id, UserId = Users.Bert.Id, Assigned = true });
            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimplePlanning.Id && p.UserId == Users.Zahir.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimplePlanning.Id, UserId = Users.Zahir.Id, Assigned = true });

            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimpleSetup.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimpleSetup.Id, UserId = Users.Alayah.Id, Assigned = true });

            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimpleDataEntry.Id && p.UserId == Users.Zahir.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimpleDataEntry.Id, UserId = Users.Zahir.Id, Assigned = true });
            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimpleDataEntry.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimpleDataEntry.Id, UserId = Users.Alayah.Id, Assigned = true });

            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimpleQA.Id && p.UserId == Users.Bert.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimpleQA.Id, UserId = Users.Bert.Id, Assigned = true });

            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimpleNotifyCompletion.Id && p.UserId == Users.Mylo.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimpleNotifyCompletion.Id, UserId = Users.Mylo.Id, Assigned = true });

            if (dbContext.PhaseAssignments.Any(p => p.PhaseId == Phases.SimplePostAnalytics.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseAssignments.Add(new PhaseAssignment { PhaseId = Phases.SimplePostAnalytics.Id, UserId = Users.Alayah.Id, Assigned = true });

			//  Save all updates
			await dbContext.SaveChangesAsync();
		}
    }
}
