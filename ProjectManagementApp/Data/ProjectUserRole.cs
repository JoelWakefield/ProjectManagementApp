namespace ProjectManagementApp.Data
{
    public class ProjectUserRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
