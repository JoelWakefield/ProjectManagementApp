namespace ProjectManagementApp.ViewModels
{
    public class ApplicationUserVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<ProjectRoleVm> ProjectRoles { get; set; }
    }
}
