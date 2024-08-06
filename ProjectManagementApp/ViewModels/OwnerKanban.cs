namespace ProjectManagementApp.ViewModels
{
	public class KanBanSection
	{
		public string Name { get; init; }
		public KanbanOwnerType Type { get; set; }

		public KanBanSection(string name, KanbanOwnerType type = KanbanOwnerType.Unowned)
		{
			Name = name;
			Type = type;
		}
	}

	public class KanbanOwnerItem
	{
		public string Name { get; init; }
		public string ItemId { get; set; }
		public string Owner { get; set; }
		public string OwnerId { get; set; }


		public KanbanOwnerItem(string name, string itemId, string owner, string ownerId)
		{
			Name = name;
			ItemId = itemId;
			Owner = owner;
			OwnerId = ownerId;
		}
	}

	public enum KanbanOwnerType
	{
		Owned,
		Unowned
	}
}
