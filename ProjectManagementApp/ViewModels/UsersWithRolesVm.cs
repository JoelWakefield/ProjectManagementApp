using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Data;

namespace ProjectManagementApp.ViewModels
{
    public class UsersWithRolesVm(UserManager<ApplicationUser> userManager)
    {
        public IEnumerable<UserWithRoles> GetUsersWithRoles()
        {
            foreach (var user in userManager.Users)
            {
                yield return new UserWithRoles(user);
            }
        }
    }
}
