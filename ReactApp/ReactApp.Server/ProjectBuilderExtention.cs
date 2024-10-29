using Microsoft.EntityFrameworkCore;
using ReactApp.Server.Data;

namespace ReactApp.Server
{
    public static class ProjectBuilderExtention
    {
        public static async Task BuildFakeProject(this WebApplication app)
        {
            //  Setup services
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //  Create initial roles (for identity and project)
            if (dbContext.ProjectRoles.Any(r => r.Name == "manager") == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = "manager" });
            if (dbContext.ProjectRoles.Any(r => r.Name == "owner") == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = "owner" });
            if (dbContext.ProjectRoles.Any(r => r.Name == "memeber") == false)
                dbContext.ProjectRoles.Add(new ProjectRole() { Name = "member" });

            //  Pull the updated/pre-existing project role data
            ProjectRole managerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == "manager")!;
            ProjectRole ownerRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == "owner")!;
            ProjectRole memberRole = dbContext.ProjectRoles.FirstOrDefault(r => r.Name == "member")!;

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Create initial users
            if (dbContext.Users.Any(u => u.Name == "Bert") == false)
                dbContext.Users.Add(new AppUser() { Name = "Bert" });
            if (dbContext.Users.Any(u => u.Name == "Mylo") == false)
                dbContext.Users.Add(new AppUser() { Name = "Mylo" });
            if (dbContext.Users.Any(u => u.Name == "Alayah") == false)
                dbContext.Users.Add(new AppUser() { Name = "Alayah" });
            if (dbContext.Users.Any(u => u.Name == "Zahir") == false)
                dbContext.Users.Add(new AppUser() { Name = "Zahir" });

            //  Pull the updated/pre-existing user data
            AppUser bert = dbContext.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.Name == "Bert")!;
            AppUser mylo = dbContext.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.Name == "Mylo")!;
            AppUser alayah = dbContext.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.Name == "Alayah")!;
            AppUser zahir = dbContext.Users.Include(u => u.ProjectRoles).FirstOrDefault(u => u.Name == "Zahir")!;

            //  Save all updates
            await dbContext.SaveChangesAsync();


            //  Assign project roles to new users
            if (mylo.ProjectRoles.Any(r => r.Id == managerRole.Id) == false)
                mylo.ProjectRoles.Add(managerRole);

            if (mylo.ProjectRoles.Any(r => r.Id == ownerRole.Id) == false)
                mylo.ProjectRoles.Add(ownerRole);
            if (alayah.ProjectRoles.Any(r => r.Id == ownerRole.Id) == false)
                alayah.ProjectRoles.Add(ownerRole);

            if (mylo.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                mylo.ProjectRoles.Add(memberRole);
            if (bert.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                bert.ProjectRoles.Add(memberRole);
            if (alayah.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                alayah.ProjectRoles.Add(memberRole);
            if (zahir.ProjectRoles.Any(r => r.Id == memberRole.Id) == false)
                zahir.ProjectRoles.Add(memberRole);

            //  Save all updates
            await dbContext.SaveChangesAsync();
        }
    }
}
