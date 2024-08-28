namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Keeps track of when people are assigned to what phase;
    /// therefore, the data is kept immutably.
    /// </summary>
    public class PhaseAssignmentArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string UserId { get; set; }
        public bool Assigned { get; set; }
    }
}
