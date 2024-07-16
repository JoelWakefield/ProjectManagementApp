using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Services
{
    public class RoleService
    {
        private RoleManager<IdentityRole> roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IEnumerable<IdentityRole> GetRoles() => roleManager.Roles;

        public async void Create([Required] string name)
        {
            if (name != null)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                    return;
                else
                    return;
            }
            return;
        }
    }
}
