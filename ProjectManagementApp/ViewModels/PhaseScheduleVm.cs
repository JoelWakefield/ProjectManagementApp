using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ViewModels
{
    public class PhaseScheduleVm
    {
        [Required]
        public string PhaseId { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }
}
