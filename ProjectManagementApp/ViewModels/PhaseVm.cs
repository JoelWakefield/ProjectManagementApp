using ProjectManagementApp.Data;

namespace ProjectManagementApp.ViewModels
{
	public class PhaseVm
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public Priority Priority { get; set; } = Priority.Medium;
		public string ProjectId { get; set; }
		public ApplicationUserVm Owner { get; set; }
        public StageVm Stage { get; set; }
    }
}
