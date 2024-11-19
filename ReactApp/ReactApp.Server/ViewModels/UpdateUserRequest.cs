namespace ReactApp.Server.ViewModels
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, bool> ProjectRoles { get; set; }
    }
}
