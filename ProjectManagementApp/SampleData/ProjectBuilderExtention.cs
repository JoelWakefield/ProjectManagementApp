using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            ApplicationUser bert = userManager.Users.FirstOrDefault(u => u.UserName == Users.Bert.UserName)!;
            ApplicationUser mylo = userManager.Users.FirstOrDefault(u => u.UserName == Users.Mylo.UserName)!;
            ApplicationUser alayah = userManager.Users.FirstOrDefault(u => u.UserName == Users.Alayah.UserName)!;
            ApplicationUser zahir = userManager.Users.FirstOrDefault(u => u.UserName == Users.Zahir.UserName)!;


            //  Assign identity roles to new users
            if ((await userManager.GetRolesAsync(mylo)).Contains(IdentityRoles.Admin) == false)
                await userManager.AddToRoleAsync(mylo, IdentityRoles.Admin);

            if ((await userManager.GetRolesAsync(mylo)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(mylo, IdentityRoles.User);
            if ((await userManager.GetRolesAsync(bert)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(bert, IdentityRoles.User);
            if ((await userManager.GetRolesAsync(alayah)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(alayah, IdentityRoles.User);
            if ((await userManager.GetRolesAsync(zahir)).Contains(IdentityRoles.User) == false)
                await userManager.AddToRoleAsync(zahir, IdentityRoles.User);

            await dbContext.SaveChangesAsync();

            bert = userManager.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.UserName == bert.UserName)!;
            mylo = userManager.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.UserName == mylo.UserName)!;
            alayah = userManager.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.UserName == alayah.UserName)!;
            zahir = userManager.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.UserName == zahir.UserName)!;

            //  Assign project roles to new users
            if (mylo.ProjectRoles.Any(r => r.Id == managerRole.Id) == false)
                mylo.ProjectRoles.Add(managerRole);
            await dbContext.SaveChangesAsync();

            if (mylo.ProjectRoles.Any(r => r.Id == ownerRole.Id) == false)
                mylo.ProjectRoles.Add(ownerRole);
            await dbContext.SaveChangesAsync();
            if (alayah.ProjectRoles.Any(r => r.Id == ownerRole.Id) == false)
                alayah.ProjectRoles.Add(ownerRole);
            await dbContext.SaveChangesAsync();

            if (mylo.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                mylo.ProjectRoles.Add(memberRole);
            await dbContext.SaveChangesAsync();
            if (bert.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                bert.ProjectRoles.Add(memberRole);
            await dbContext.SaveChangesAsync();
            if (alayah.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                alayah.ProjectRoles.Add(memberRole);
            await dbContext.SaveChangesAsync();
            if (zahir.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                zahir.ProjectRoles.Add(memberRole);

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

            var simpleProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.SimpleProject.Name)!;
            var multiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPhaseProject.Name)!;
            var multiPersonProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPersonProject.Name)!;
            var multiPersonMultiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == Projects.MultiPersonMultiPhaseProject.Name)!;


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

            var stageBacklog = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Backlog.Name)!;
            var stagesToDo = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.ToDo.Name)!;
            var stagesInProgress = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.InProgress.Name)!;
            var stagesReview = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Review.Name)!;
            var stageComplete = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Complete.Name)!;
            var stagesCanceled = dbContext.Stages.FirstOrDefault(s => s.Name == Stages.Canceled.Name)!;


            //  Add/Update Phases
            if (dbContext.Phases.Any(p => p.ProjectId == simpleProject.Id && p.Name == PhaseData.SimplePlanning().Name) == false)
            {
                //var project = dbContext.Projects.Include(p => p.Phases).Where(p => p.Id == simpleProject.Id).First();
                var temp = PhaseData.SimplePlanning();
                dbContext.Add(new Phase { Description = "desc", Name = "name", OwnerId = $"{temp.OwnerId}", ProjectId = $"{simpleProject.Id}", StageId = $"{stageComplete.Id}" });
                await dbContext.SaveChangesAsync();
            }
            if (dbContext.Phases.Any(p => p.ProjectId == simpleProject.Id && p.Name == PhaseData.SimpleSetup.Name) == false)
                dbContext.Phases.Add(PhaseData.SimpleSetup);
            if (dbContext.Phases.Any(p => p.ProjectId == simpleProject.Id && p.Name == PhaseData.SimpleDataEntry.Name) == false)
                dbContext.Phases.Add(PhaseData.SimpleDataEntry);
            if (dbContext.Phases.Any(p => p.ProjectId == simpleProject.Id && p.Name == PhaseData.SimpleQA.Name) == false)
                dbContext.Phases.Add(PhaseData.SimpleQA);
            if (dbContext.Phases.Any(p => p.ProjectId == simpleProject.Id && p.Name == PhaseData.SimpleNotifyCompletion.Name) == false)
                dbContext.Phases.Add(PhaseData.SimpleNotifyCompletion);
            if (dbContext.Phases.Any(p => p.ProjectId == simpleProject.Id && p.Name == PhaseData.SimplePostAnalytics.Name) == false)
                dbContext.Phases.Add(PhaseData.SimplePostAnalytics);

            await dbContext.SaveChangesAsync();

            Phase simplePlanningPhase = dbContext.Phases.FirstOrDefault(p => p.Name == PhaseData.SimplePlanning().Name)!;
            Phase simpleSetupPhase = dbContext.Phases.FirstOrDefault(p => p.Name == PhaseData.SimpleSetup.Name)!;
            Phase simpleDataEntryPhase = dbContext.Phases.FirstOrDefault(p => p.Name == PhaseData.SimpleDataEntry.Name)!;
            Phase simpleQAPhase = dbContext.Phases.FirstOrDefault(p => p.Name == PhaseData.SimpleQA.Name)!;
            Phase simpleNotifyCompletionPhase = dbContext.Phases.FirstOrDefault(p => p.Name == PhaseData.SimpleNotifyCompletion.Name)!;
            Phase simplePostAnalyticsPhase = dbContext.Phases.FirstOrDefault(p => p.Name == PhaseData.SimplePostAnalytics.Name)!;


            //  Assign users to phases
            if (simplePlanningPhase.Assignments.Any(a => a.Id == alayah.Id) == false)
                simplePlanningPhase.Assignments.Add(alayah);
            if (simplePlanningPhase.Assignments.Any(a => a.Id == bert.Id) == false)
                simplePlanningPhase.Assignments.Add(bert);
            if (simplePlanningPhase.Assignments.Any(a => a.Id == zahir.Id) == false)
                simplePlanningPhase.Assignments.Add(zahir);
            if (simpleSetupPhase.Assignments.Any(a => a.Id == alayah.Id) == false)
                simpleSetupPhase.Assignments.Add(alayah);
            if (simpleDataEntryPhase.Assignments.Any(a => a.Id == zahir.Id) == false)
                simpleDataEntryPhase.Assignments.Add(zahir);
            if (simpleDataEntryPhase.Assignments.Any(a => a.Id == alayah.Id) == false)
                simpleDataEntryPhase.Assignments.Add(alayah);
            if (simpleQAPhase.Assignments.Any(a => a.Id == bert.Id) == false)
                simpleQAPhase.Assignments.Add(bert);
            if (simpleNotifyCompletionPhase.Assignments.Any(a => a.Id == mylo.Id) == false)
                simpleNotifyCompletionPhase.Assignments.Add(mylo);
            if (simplePostAnalyticsPhase.Assignments.Any(a => a.Id == alayah.Id) == false)
                simplePostAnalyticsPhase.Assignments.Add(alayah);

            await dbContext.SaveChangesAsync();


            //  Create Schedules for phases
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simplePlanningPhase.Id && p.UserId == alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePlanningAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simplePlanningPhase.Id && p.UserId == bert.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePlanningBertSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simplePlanningPhase.Id && p.UserId == zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePlanningZahirSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simpleSetupPhase.Id && p.UserId == alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleSetupAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simpleSetupPhase.Id && p.UserId == zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleSetupZahirSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simpleDataEntryPhase.Id && p.UserId == alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleDataEntryAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simpleDataEntryPhase.Id && p.UserId == zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleDataEntryZahirSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simpleQAPhase.Id && p.UserId == bert.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleQABertSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simpleNotifyCompletionPhase.Id && p.UserId == alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleNotifyCompletionAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simpleNotifyCompletionPhase.Id && p.UserId == mylo.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimpleNotifyCompletionMyloSchedule);

            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simplePostAnalyticsPhase.Id && p.UserId == alayah.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePostAnalyticsAlayahSchedule);
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == simplePostAnalyticsPhase.Id && p.UserId == zahir.Id) == false)
                dbContext.PhaseSchedules.Add(PhaseSchedules.SimplePostAnalyticsZahirSchedule);

            await dbContext.SaveChangesAsync();
        }
    }
}
