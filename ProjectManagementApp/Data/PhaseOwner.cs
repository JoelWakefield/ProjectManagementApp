namespace ProjectManagementApp.Data
{
    public class PhaseOwner
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ArchiveId { get; set; }
        public string PhaseId { get; set; }
        public Phase Phase { get; set; }
        public string UserId { get; set; }
        public ApplicationUser Owner { get; set; }
    }

    public class PhaseOwnerArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string UserId { get; set; }
    }
}
