using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ViewModels
{
    public class RegisterRoleForm
    {
        [Required]
        [StringLength(256, ErrorMessage = "Name length can't be more than 256 characters.")]
        public string? Name { get; set; }
    }

    public class RegisterStageForm
    {
        [Required]
        [StringLength(256, ErrorMessage = "Name length can't be more than 256 characters.")]
        public string? Name { get; set; }
    }
}
