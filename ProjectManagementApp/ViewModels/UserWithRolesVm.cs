using ProjectManagementApp.Data;

namespace ProjectManagementApp.ViewModels
{
    public class UserWithRolesVm
    {
        public ApplicationUser User { get; set; }
        public string? Roles { get; set; }
    }
}
