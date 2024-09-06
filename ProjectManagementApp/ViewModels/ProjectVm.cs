namespace ProjectManagementApp.ViewModels
{
	public class ProjectVm
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public DateTime ProjectedStart { get; set; }
		public DateTime ProjectedEnd { get; set; }
		public DateTime ActualStart { get; set; }
		public DateTime ActualEnd { get; set; }
		public float TotalWorkHours { get; set; }

		public IEnumerable<PhaseVm> Phases { get; set; }
		public ApplicationUserVm Owner { get; set; }
	}
}
