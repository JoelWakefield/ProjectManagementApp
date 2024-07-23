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

            // Pull the complete user data
            Users.Bert = userManager.Users.FirstOrDefault(u => u.UserName == Users.Bert.UserName)!;
            Users.Mylo = userManager.Users.FirstOrDefault(u => u.UserName == Users.Mylo.UserName)!;
            Users.Alayah = userManager.Users.FirstOrDefault(u => u.UserName == Users.Alayah.UserName)!;
            Users.Zahir = userManager.Users.FirstOrDefault(u => u.UserName == Users.Zahir.UserName)!;

            // Assign roles to new users
            if ((await userManager.GetRolesAsync(Users.Bert)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Bert, Roles.Member);

            if ((await userManager.GetRolesAsync(Users.Mylo)).Contains(Roles.Manager) == false)
                await userManager.AddToRoleAsync(Users.Mylo, Roles.Manager);
            if ((await userManager.GetRolesAsync(Users.Mylo)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Mylo, Roles.Manager);

            if ((await userManager.GetRolesAsync(Users.Alayah)).Contains(Roles.Lead) == false)
                await userManager.AddToRoleAsync(Users.Alayah, Roles.Lead);
            if ((await userManager.GetRolesAsync(Users.Alayah)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Alayah, Roles.Member);

            if ((await userManager.GetRolesAsync(Users.Zahir)).Contains(Roles.Member) == false)
                await userManager.AddToRoleAsync(Users.Zahir, Roles.Manager);

            // Setup initial projects
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.SimpleProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.SimpleProject);
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.MultiPhaseProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.MultiPhaseProject);
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.MultiPersonProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.MultiPersonProject);
            if (dbContext.Projects.Any(p => p.Name == ProjectConstants.MultiPersonMultiPhaseProject.Name) == false)
                dbContext.Projects.Add(ProjectConstants.MultiPersonMultiPhaseProject);

            await dbContext.SaveChangesAsync();
        }
    }
}
