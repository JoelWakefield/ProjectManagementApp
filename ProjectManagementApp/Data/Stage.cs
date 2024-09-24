using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    /// <summary>
    /// A separate table called `PhaseStages` will keep track of phase stage changes; 
    /// the current phase will simply be the most recent record for said phase.
    /// </summary>
    public class Stage
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public required string Name { get; set; }
        [Required]
        public required int OrderId { get; set; }

        public List<Phase> Phases { get; set; } = new List<Phase>();
    }

    public class StageArchive
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Required]
        public string Name { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
