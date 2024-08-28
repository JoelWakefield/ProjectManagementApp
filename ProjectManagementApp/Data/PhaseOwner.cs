namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Keeps track of who owns what phase and when; 
    /// therefore, the data is kept immutably.
    /// </summary>
    public class PhaseOwnerArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string UserId { get; set; }
    }
}
