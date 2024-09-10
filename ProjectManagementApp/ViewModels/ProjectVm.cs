using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.ViewModels
{
	public class CreateProjectVm
	{
		[Required]
		[StringLength(256, ErrorMessage = "Name length can't be more than {1} characters.")]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string Description { get; set; } = string.Empty;
		[Required]
		public DateTime ProjectedStart { get; set; }
		[Required]
		public DateTime ProjectedEnd { get; set; }
		public DateTime ActualStart { get; set; }
		public DateTime ActualEnd { get; set; }
		public float TotalWorkHours { get; set; }

		public IEnumerable<PhaseVm>? Phases { get; set; }
		public ApplicationUserVm? Owner { get; set; }
	}

	public class ProjectVm : CreateProjectVm
	{
		[Required]
		public string Id { get; set; } = string.Empty;
	}
}
