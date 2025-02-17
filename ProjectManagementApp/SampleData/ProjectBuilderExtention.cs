using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class ProjectBuilderExtention
    {
        public static async Task BuildFakeProject(this WebApplication app)
        {
            //  Setup services
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //  Create initial roles (for identity and project)
            if (dbContext.ProjectRoles.Any(r => r.Name == ProjectRoles.Manager) == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = ProjectRoles.Manager });
            if (dbContext.ProjectRoles.Any(r => r.Name == ProjectRoles.Owner) == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = ProjectRoles.Owner });
            if (dbContext.ProjectRoles.Any(r => r.Name == ProjectRoles.Member) == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = ProjectRoles.Member });

            //  Create initial users
            if (dbContext.Users.Any(u => u.UserName == Users.Bert.UserName) == false)
                dbContext.Add(Users.Bert);
            if (dbContext.Users.Any(u => u.UserName == Users.Mylo.UserName) == false)
                dbContext.Add(Users.Mylo);
            if (dbContext.Users.Any(u => u.UserName == Users.Alayah.UserName) == false)
                dbContext.Add(Users.Alayah);
            if (dbContext.Users.Any(u => u.UserName == Users.Zahir.UserName) == false)
                dbContext.Add(Users.Zahir);

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Pull the updated/pre-existing user data
            Users.Bert = dbContext.Users.FirstOrDefault(u => u.UserName == Users.Bert.UserName)!;
            Users.Mylo = dbContext.Users.FirstOrDefault(u => u.UserName == Users.Mylo.UserName)!;
            Users.Alayah = dbContext.Users.FirstOrDefault(u => u.UserName == Users.Alayah.UserName)!;
            Users.Zahir = dbContext.Users.FirstOrDefault(u => u.UserName == Users.Zahir.UserName)!;

            //  Pull the updated/pre-existing project role data
            ProjectRole managerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Manager)!;
            ProjectRole ownerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Owner)!;
            ProjectRole memberRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Member)!;

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


            //  Create Schedules for phases
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimplePlanning.Id) == false)
                dbContext.PhaseSchedules.Add(new PhaseSchedule { PhaseId = Phases.SimplePlanning.Id, Start = DateTime.UtcNow.AddDays(-8), End = DateTime.UtcNow.AddDays(-6) });
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleSetup.Id) == false)
                dbContext.PhaseSchedules.Add(new PhaseSchedule { PhaseId = Phases.SimpleSetup.Id, Start = DateTime.UtcNow.AddDays(-5), End = DateTime.UtcNow.AddDays(-2) });
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleDataEntry.Id) == false)
                dbContext.PhaseSchedules.Add(new PhaseSchedule { PhaseId = Phases.SimpleDataEntry.Id, Start = DateTime.UtcNow.AddDays(-1), End = DateTime.UtcNow.AddDays(4) });
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleQA.Id) == false)
                dbContext.PhaseSchedules.Add(new PhaseSchedule { PhaseId = Phases.SimpleQA.Id, Start = DateTime.UtcNow.AddDays(5), End = DateTime.UtcNow.AddDays(6) });
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimpleNotifyCompletion.Id) == false)
                dbContext.PhaseSchedules.Add(new PhaseSchedule { PhaseId = Phases.SimpleNotifyCompletion.Id, Start = DateTime.UtcNow.AddDays(7), End = DateTime.UtcNow.AddDays(7) });
            if (dbContext.PhaseSchedules.Any(p => p.PhaseId == Phases.SimplePostAnalytics.Id) == false)
                dbContext.PhaseSchedules.Add(new PhaseSchedule { PhaseId = Phases.SimplePostAnalytics.Id, Start = DateTime.UtcNow.AddDays(7), End = DateTime.UtcNow.AddDays(10) });

            //  Save all updates
            await dbContext.SaveChangesAsync();
        }
    }
}
