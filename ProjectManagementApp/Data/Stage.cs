using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    /// <summary>
    /// A separate table called  `PhaseStages` will keep track of phase stage changes; the current phase will simply be the most recent record for said phase.
    /// </summary>
    public class Stage
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
    }
}
