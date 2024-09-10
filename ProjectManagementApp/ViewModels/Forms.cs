using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ViewModels
{
    public class RegisterRoleForm
    {
        [Required]
        [StringLength(256, ErrorMessage = "Name length can't be more than {1} characters.")]
        public string Name { get; set; } = string.Empty;
    }

    public class RegisterStageForm
    {
        [Required]
        [StringLength(256, ErrorMessage = "Name length can't be more than {1} characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
