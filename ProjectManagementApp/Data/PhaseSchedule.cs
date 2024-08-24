namespace ProjectManagementApp.Data
{
	public class PhaseSchedule
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }
}
