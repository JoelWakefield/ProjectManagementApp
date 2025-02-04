namespace ReactApp.Server.ViewModels
{
    public class EditProjectVm
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateOnly ProjectedStart { get; set; }
        public DateOnly ProjectedEnd { get; set; }
    }
}
