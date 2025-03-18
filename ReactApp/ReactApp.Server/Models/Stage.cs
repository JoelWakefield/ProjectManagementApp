using System.ComponentModel.DataAnnotations;

namespace ReactApp.Server.Models
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
        [Required]
        public string Name { get; set; }
        [Required]
        public int OrderId { get; set; }

        public ICollection<Phase> Phases { get; set; }
    }
}
