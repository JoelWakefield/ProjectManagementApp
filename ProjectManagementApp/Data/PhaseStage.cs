namespace ProjectManagementApp.Data
{
    public class PhaseStage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ArchiveId { get; set; }
        public string PhaseId { get; set; }
        public Phase Phase { get; set; }
        public string StageId { get; set; }
        public Stage Stage { get; set; }
    }
    public class PhaseStageArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string StageId { get; set; }
    }
}
