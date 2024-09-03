using ProjectManagementApp.Data;

namespace ProjectManagementApp.ViewModels
{
    public class UserWithRoles
    {
        public ApplicationUser User { get; set; }
        public string? Roles { get; set; }

        public UserWithRoles(ApplicationUser user)
        {
            User = user;
            IEnumerable<string> roles = user.ProjectRoles.Select(r => r.Name);
            Roles = roles.Any() == true ? String.Join(", ", roles) : "";
        }
    }
}
