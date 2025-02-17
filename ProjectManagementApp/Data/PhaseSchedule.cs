namespace ProjectManagementApp.Data
{
	public class PhaseSchedule
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        //public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
