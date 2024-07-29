using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.SampleData
{
    public static class ProjectBuilderExtention
    {
        public static async Task BuildFakeProject(this WebApplication app)
        {
            // Setup services
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Create initial roles (for identity and project)
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

            // Create initial users
            if (userManager.Users.Any(u => u.UserName == Users.Bert.UserName) == false)
                await userManager.CreateAsync(Users.Bert);
            if (userManager.Users.Any(u => u.UserName == Users.Mylo.UserName) == false)
                await userManager.CreateAsync(Users.Mylo);
            if (userManager.Users.Any(u => u.UserName == Users.Alayah.UserName) == false)
                await userManager.CreateAsync(Users.Alayah);
            if (userManager.Users.Any(u => u.UserName == Users.Zahir.UserName) == false)
                await userManager.CreateAsync(Users.Zahir);

            // Pull the updated/pre-existing user data
            Users.Bert = userManager.Users.FirstOrDefault(u => u.UserName == Users.Bert.UserName)!;
            Users.Mylo = userManager.Users.FirstOrDefault(u => u.UserName == Users.Mylo.UserName)!;
            Users.Alayah = userManager.Users.FirstOrDefault(u => u.UserName == Users.Alayah.UserName)!;
            Users.Zahir = userManager.Users.FirstOrDefault(u => u.UserName == Users.Zahir.UserName)!;

            // Pull the updated/pre-existing project role data
            ProjectRole managerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Manager)!;
            ProjectRole ownerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Owner)!;
            ProjectRole memberRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == ProjectRoles.Member)!;

            // Assign identity roles to new users
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

            // Assign project roles to new users
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

            // Setup initial projects
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.SimpleProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.SimpleProject);
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.MultiPhaseProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.MultiPhaseProject);
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.MultiPersonProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.MultiPersonProject);
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.MultiPersonMultiPhaseProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.MultiPersonMultiPhaseProject);

            // Pull the updated/pre-existing project info
            ProjectConstants.SimpleProject = dbContext.Projects.FirstOrDefault(p => p.Name == ProjectConstants.SimpleProject.Name)!;
            ProjectConstants.MultiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == ProjectConstants.MultiPhaseProject.Name)!;
            ProjectConstants.MultiPersonProject = dbContext.Projects.FirstOrDefault(p => p.Name == ProjectConstants.MultiPersonProject.Name)!;
            ProjectConstants.MultiPersonMultiPhaseProject = dbContext.Projects.FirstOrDefault(p => p.Name == ProjectConstants.MultiPersonMultiPhaseProject.Name)!;

            // Setup owners with projects (if a project exists, then it already has an owner - no need to add another record)
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == ProjectConstants.SimpleProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner() 
                {
                    ProjectId = ProjectConstants.SimpleProject.Id,
                    UserId = Users.Mylo.Id,
                });
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == ProjectConstants.MultiPhaseProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = ProjectConstants.MultiPhaseProject.Id,
                    UserId = Users.Mylo.Id,
                });
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == ProjectConstants.MultiPersonProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = ProjectConstants.MultiPersonProject.Id,
                    UserId = Users.Alayah.Id,
                });
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == ProjectConstants.MultiPersonMultiPhaseProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = ProjectConstants.MultiPersonMultiPhaseProject.Id,
                    UserId = Users.Alayah.Id,
                });

            // Save all updates
            await dbContext.SaveChangesAsync();
        }
    }
}
