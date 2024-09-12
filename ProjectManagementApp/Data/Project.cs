using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    /// <summary>
    /// The Project entity keeps track of broad data which isn't likely to change and whose changes aren't worth tracking.
    /// </summary>
    public class Project
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Required]
        public required string CreatedBy { get; set; }

        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public DateTime ProjectedStart { get; set; }
        [Required]
        public DateTime ProjectedEnd { get; set; }
        public DateTime ActualStart { get; set; }
        public DateTime ActualEnd { get; set; }
        public float TotalWorkHours { get; set; }

        [Required]
        public required string OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }

        public List<Phase>? Phases { get; set; } = new List<Phase>();
    }

    public class ProjectArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ProjectedStart { get; set; }
        public DateTime ProjectedEnd { get; set; }
        public DateTime ActualStart { get; set; }
        public DateTime ActualEnd { get; set; }
        public float TotalWorkHours { get; set; }
    }
}
