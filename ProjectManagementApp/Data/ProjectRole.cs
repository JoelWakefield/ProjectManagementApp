namespace ProjectManagementApp.Data
{
    public class ProjectRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}
