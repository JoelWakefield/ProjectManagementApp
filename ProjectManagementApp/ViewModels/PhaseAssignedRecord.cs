namespace ProjectManagementApp.ViewModels
{
	/// <summary>
	/// Keeps a record of which users are assigned to a specific phase
	/// </summary>
	public class PhaseAssignedRecord
	{
		public string PhaseId { get; set; }
		public string Name { get; set; }
        public Dictionary<string, bool> Assignments { get; set; }

		public PhaseAssignedRecord(string phaseId, string name)
		{
			PhaseId = phaseId;
			Name = name;
			Assignments = new Dictionary<string, bool>();
		}
    }
}
