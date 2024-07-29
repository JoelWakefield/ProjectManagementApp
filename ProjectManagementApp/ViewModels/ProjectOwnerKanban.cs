namespace ProjectManagementApp.ViewModels
{
	public class KanBanSection
	{
		public string Name { get; init; }
        public KanbanProjectType Type { get; set; }

        public KanBanSection(string name, KanbanProjectType type = KanbanProjectType.Unowned)
		{
			Name = name;
			Type = type;
		}
	}

	public class KanbanProjectItem
	{
		public string Name { get; init; }
		public string ProjectId { get; set; }
        public string Owner { get; set; }
        public string OwnerId { get; set; }


        public KanbanProjectItem(string name, string projectId, string owner, string ownerId)
		{
			Name = name;
			ProjectId = projectId;
			Owner = owner;
			OwnerId = ownerId;
		}
	}

	public enum KanbanProjectType
	{
		Owned,
		Unowned
	}
}
