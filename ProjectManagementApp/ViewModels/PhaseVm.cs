using ProjectManagementApp.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ViewModels
{
	public class CreatePhaseVm
	{
		[Required]
		[StringLength(256, ErrorMessage = "Name length can't be more than {1} characters.")]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string Description { get; set; } = string.Empty;
		public Priority Priority { get; set; } = Priority.Medium;
		[Required]
		public string ProjectId { get; set; } = string.Empty;
		[Required]
		public string OwnerId { get; set; } = string.Empty;
		public ApplicationUserVm Owner { get; set; } = new ApplicationUserVm();
		[Required]
        public string StageId { get; set; } = string.Empty;
    }

	public class PhaseVm : CreatePhaseVm
	{
		[Required]
		public string Id { get; set; } = string.Empty;
	}
}
