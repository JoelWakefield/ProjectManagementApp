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
        public string ProjectId { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
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
