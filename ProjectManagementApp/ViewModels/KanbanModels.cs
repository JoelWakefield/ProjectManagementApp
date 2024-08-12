namespace ProjectManagementApp.ViewModels
{
	public class KanBanSection
	{
		public string Name { get; init; }
		public bool Categorized { get; set; }

		public KanBanSection(string name, bool categorized = false)
		{
			Name = name;
			Categorized = categorized;
		}
	}

	public class KanbanItem
	{
		public string Name { get; init; }
		public string ItemId { get; set; }
		public string Category { get; set; }
		public string CategoryId { get; set; }


		public KanbanItem(string name, string itemId, string category, string categoryId)
		{
			Name = name;
			ItemId = itemId;
			Category = category;
			CategoryId = categoryId;
		}
	}
}
