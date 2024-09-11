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

            await dbContext.SaveChangesAsync();

            ProjectRole managerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Manager)!;
            ProjectRole ownerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Owner)!;
            ProjectRole memberRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Member)!;


            //  Add/Update Users
            if (userManager.Users.Any(u => u.UserName == Users.Bert.UserName) == false)
                await userManager.CreateAsync(Users.Bert);
            if (userManager.Users.Any(u => u.UserName == Users.Mylo.UserName) == false)
                await userManager.CreateAsync(Users.Mylo);
            if (userManager.Users.Any(u => u.UserName == Users.Alayah.UserName) == false)
                await userManager.CreateAsync(Users.Alayah);
            if (userManager.Users.Any(u => u.UserName == Users.Zahir.UserName) == false)
                await userManager.CreateAsync(Users.Zahir);

            await dbContext.SaveChangesAsync();

            Users.Bert = userManager.Users.FirstOrDefault(u => u.UserName == Users.Bert.UserName)!;
            Users.Mylo = userManager.Users.FirstOrDefault(u => u.UserName == Users.Mylo.UserName)!;
            Users.Alayah = userManager.Users.FirstOrDefault(u => u.UserName == Users.Alayah.UserName)!;
            Users.Zahir = userManager.Users.FirstOrDefault(u => u.UserName == Users.Zahir.UserName)!;


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

            await dbContext.SaveChangesAsync();


            //  Assign project roles to new users
            if (Users.Mylo.ProjectRoles.Any(r => r.Id == managerRole.Id) == false)
            {
                Users.Mylo.ProjectRoles.Add(managerRole);
                managerRole.Users.Add(Users.Mylo);
            }

            if (Users.Mylo.ProjectRoles.Any(r => r.Id == ownerRole.Id) == false)
            {
                Users.Mylo.ProjectRoles.Add(ownerRole);
                ownerRole.Users.Add(Users.Mylo);
            }
            if (Users.Alayah.ProjectRoles.Any(r => r.Id == ownerRole.Id) == false)
            {
                Users.Alayah.ProjectRoles.Add(ownerRole);
                ownerRole.Users.Add(Users.Alayah);
            }

            if (Users.Mylo.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
            {
                Users.Mylo.ProjectRoles.Add(memberRole);
                managerRole.Users.Add(Users.Mylo);
            }
            if (Users.Bert.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
            {
                Users.Bert.ProjectRoles.Add(memberRole);
                memberRole.Users.Add(Users.Bert);
            }
            if (Users.Alayah.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
            {
                Users.Alayah.ProjectRoles.Add(memberRole);
                memberRole.Users.Add(Users.Alayah);
            }
            if (Users.Zahir.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
            {
                Users.Zahir.ProjectRoles.Add(memberRole);
                memberRole.Users.Add(Users.Zahir);
            }

            await dbContext.SaveChangesAsync();


            //  Add/Update Projects
            if (dbContext.Projects.Any(p => p.Name == Projects.SimpleProject.Name) == false)
                dbContext.Projects.Add(Projects.SimpleProject);
            if (dbContext.Projects.Any(p => p.Name == Projects.MultiPhaseProject.Name) == false)
                dbContext.Projects.Add(Projects.MultiPhaseProject);
            if (dbContext.Projects.Any(p => p.Name == Projects.MultiPersonProject.Name) == false)
                dbContext.Projects.Add(Projects.MultiPersonProject);
            if (dbContext.Projects.Any(p => p.Name == Projects.MultiPersonMultiPhaseProject.Name) == false)
                dbContext.Projects.Add(Projects.MultiPersonMultiPhaseProject);

            await dbContext.SaveChangesAsync();

            Projects.SimpleProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.SimpleProject.Name)!;
            Projects.MultiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPhaseProject.Name)!;
            Projects.MultiPersonProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPersonProject.Name)!;
            Projects.MultiPersonMultiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPersonMultiPhaseProject.Name)!;


            //  Add/Update Stages
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

            await dbContext.SaveChangesAsync();

            Stages.Backlog = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Backlog.Name)!;
            Stages.ToDo = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.ToDo.Name)!;
            Stages.InProgress = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.InProgress.Name)!;
            Stages.Review = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Review.Name)!;
            Stages.Complete = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Complete.Name)!;
            Stages.Canceled = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Canceled.Name)!;


            //  Add/Update Phases
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

            await dbContext.SaveChangesAsync();

            Phases.SimplePlanning = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimplePlanning.Name)!;
            Phases.SimpleSetup = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleSetup.Name)!;
            Phases.SimpleDataEntry = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleDataEntry.Name)!;
            Phases.SimpleQA = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleQA.Name)!;
            Phases.SimpleNotifyCompletion = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimpleNotifyCompletion.Name)!;
            Phases.SimplePostAnalytics = dbContext.Phases.FirstOrDefault(p => p.Name == Phases.SimplePostAnalytics.Name)!;


            //  Assign users to phases
            if (Phases.SimplePlanning.Assignments.Any(a => a.Id == Users.Alayah.Id) == false)
                Phases.SimplePlanning.Assignments.Add(Users.Alayah);
            if (Phases.SimplePlanning.Assignments.Any(a => a.Id == Users.Bert.Id) == false)
                Phases.SimplePlanning.Assignments.Add(Users.Bert);
            if (Phases.SimplePlanning.Assignments.Any(a => a.Id == Users.Zahir.Id) == false)
                Phases.SimplePlanning.Assignments.Add(Users.Zahir);

            if (Phases.SimpleSetup.Assignments.Any(a => a.Id == Users.Alayah.Id) == false)
                Phases.SimpleSetup.Assignments.Add(Users.Alayah);

            if (Phases.SimpleDataEntry.Assignments.Any(a => a.Id == Users.Zahir.Id) == false)
                Phases.SimpleDataEntry.Assignments.Add(Users.Zahir);
            if (Phases.SimpleDataEntry.Assignments.Any(a => a.Id == Users.Alayah.Id) == false)
                Phases.SimpleDataEntry.Assignments.Add(Users.Alayah);

            if (Phases.SimpleQA.Assignments.Any(a => a.Id == Users.Bert.Id) == false)
                Phases.SimpleQA.Assignments.Add(Users.Bert);

            if (Phases.SimpleNotifyCompletion.Assignments.Any(a => a.Id == Users.Mylo.Id) == false)
                Phases.SimpleNotifyCompletion.Assignments.Add(Users.Mylo);

            if (Phases.SimplePostAnalytics.Assignments.Any(a => a.Id == Users.Alayah.Id) == false)
                Phases.SimplePostAnalytics.Assignments.Add(Users.Alayah);

            await dbContext.SaveChangesAsync();


            //  Create Schedules for phases
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimplePlanning.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePlanningAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimplePlanning.Id && p.UserId == Users.Bert.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePlanningBertSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimplePlanning.Id && p.UserId == Users.Zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePlanningZahirSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleSetup.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleSetupAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleSetup.Id && p.UserId == Users.Zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleSetupZahirSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleDataEntry.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleDataEntryAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleDataEntry.Id && p.UserId == Users.Zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleDataEntryZahirSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleQA.Id && p.UserId == Users.Bert.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleQABertSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleNotifyCompletion.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleNotifyCompletionAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleNotifyCompletion.Id && p.UserId == Users.Mylo.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleNotifyCompletionMyloSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimplePostAnalytics.Id && p.UserId == Users.Alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePostAnalyticsAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimplePostAnalytics.Id && p.UserId == Users.Zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePostAnalyticsZahirSchedule);

            await dbContext.SaveChangesAsync();
        }
    }
}
