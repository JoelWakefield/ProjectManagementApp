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

            // Create initial roles
            if (roleManager.RoleExistsAsync(Roles.Admin).Result == false)
                roleManager.CreateAsync(new IdentityRole(Roles.Admin)).Wait();
            if (roleManager.RoleExistsAsync(Roles.Manager).Result == false)
                roleManager.CreateAsync(new IdentityRole(Roles.Manager)).Wait();
            if (roleManager.RoleExistsAsync(Roles.Lead).Result == false)
                roleManager.CreateAsync(new IdentityRole(Roles.Lead)).Wait();
            if (roleManager.RoleExistsAsync(Roles.Member).Result == false)
                roleManager.CreateAsync(new IdentityRole(Roles.Member)).Wait();

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

            // Assign roles to new users
            if ((await userManager.GetRolesAsync(Users.Bert)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Bert, Roles.Member);
            if ((await userManager.GetRolesAsync(Users.Bert)).Contains(Roles.Lead) == false)
                await userManager.AddToRoleAsync(Users.Bert, Roles.Lead);

            if ((await userManager.GetRolesAsync(Users.Mylo)).Contains(Roles.Manager) == false)
                await userManager.AddToRoleAsync(Users.Mylo, Roles.Manager);
            if ((await userManager.GetRolesAsync(Users.Mylo)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Mylo, Roles.Member);

            if ((await userManager.GetRolesAsync(Users.Alayah)).Contains(Roles.Lead) == false)
                await userManager.AddToRoleAsync(Users.Alayah, Roles.Lead);
            if ((await userManager.GetRolesAsync(Users.Alayah)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Alayah, Roles.Member);

            if ((await userManager.GetRolesAsync(Users.Zahir)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Zahir, Roles.Member);

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
                    UserId = Users.Bert.Id,
                });
            if (dbContext.ProjectOwners.Any(p => p.ProjectId == ProjectConstants.MultiPhaseProject.Id) == false)
                dbContext.ProjectOwners.Add(new ProjectOwner()
                {
                    ProjectId = ProjectConstants.MultiPhaseProject.Id,
                    UserId = Users.Bert.Id,
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
