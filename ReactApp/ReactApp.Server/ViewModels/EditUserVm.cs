namespace ReactApp.Server.ViewModels
{
    public class EditUserVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<EditUserProjectRole> ProjectRoles { get; set; }

    }
    public class EditUserProjectRole
    {
        public string Name { get; set; }
        public bool Value { get; set; } = false;
    }
}
