namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Keeps track of who owns what project and when; 
    /// therefore, the data is kept immutably.
    /// </summary>
    public class ProjectOwnerArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string ProjectId { get; set; }
        public string UserId { get; set; }
    }
}
