using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp.Server.Models
{
    /// <summary>
    /// Each project is worked in phases, which have priority and can be grouped in stages; phases are defined and ordered at the discretion of the app user.
    /// </summary>
    [Table("Phases")]
    public class Phase
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;

        public string? ProjectId { get; set; }
        public Project? Project { get; set; }

        public string? OwnerId { get; set; }
        public AppUser? Owner { get; set; }

        public string? StageId { get; set; }
        public Stage? Stage { get; set; }
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
