using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
	public class PhaseSchedule
	{
		[Required]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[Required]
        public required string PhaseId { get; set; }
        public Phase? Phase { get; set; }
		[Required]
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class PhaseScheduleArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string PhaseId { get; set; }
        public string UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
