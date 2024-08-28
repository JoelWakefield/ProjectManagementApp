namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Keeps track of what permissions users have and when;
    /// therefore, the data is kept immutably.
    /// </summary>
    public class ProjectUserRoleArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool Assigned { get; set; }
    }
}
