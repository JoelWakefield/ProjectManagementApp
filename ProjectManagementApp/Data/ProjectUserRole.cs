namespace ProjectManagementApp.Data
{
    public class ProjectUserRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ArchiveId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string RoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }

    public class ProjectUserRoleArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
