namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Keeps track of when a phase is assigned a stage;
    /// therefore, the data is kept immutably.
    /// </summary>
    public class PhaseStageArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string StageId { get; set; }
    }
}
