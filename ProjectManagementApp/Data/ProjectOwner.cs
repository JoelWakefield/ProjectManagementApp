namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Since Projects can change owner, it's important to keep track of who owns what and when; therefore, the data is kept immutably (ie, old records are not updated/deleted, new records are always made instead).
    /// </summary>
    public class ProjectOwner
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string ProjectId { get; set; }
        public string UserId { get; set; }
    }
}
