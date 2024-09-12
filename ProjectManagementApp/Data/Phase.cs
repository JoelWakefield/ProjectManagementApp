using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Each project is worked in phases, which have priority and can be grouped in stages; phases are defined and ordered at the discretion of the app user.
    /// </summary>
    public class Phase
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;

        [Required]
        public string ProjectId { get; set; }
        public Project? Project { get; set; }
        [Required]
        public string StageId { get; set; }
        public Stage? Stage { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }

        public List<ApplicationUser>? Assignments { get; set; }
        public List<PhaseSchedule>? Schedules { get; set; }
    }

    public class PhaseArchive
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;

        public string ProjectId { get; set; }
        public string StageId { get; set; }
        public string OwnerId { get; set; }
    }

    public enum Priority
    {
        None,
        Low,
        Medium,
        High,
        Critical
    }
}
