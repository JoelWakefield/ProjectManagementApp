using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    /// <summary>
    /// A separate table called `PhaseStages` will keep track of phase stage changes; the current phase will simply be the most recent record for said phase.
    /// 
    /// The order id must be unique, and 
    /// </summary>
    public class Stage
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ArchiveId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int OrderId { get; set; }

        public IEnumerable<Phase> Phases { get; set; }
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
