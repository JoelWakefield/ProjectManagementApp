namespace ProjectManagementApp.Data
{
	public class PhaseAssignment
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string UserId { get; set; }
        public bool Assigned { get; set; }
    }
}
