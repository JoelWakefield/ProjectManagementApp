namespace ReactApp.Server.ViewModels
{
    public class CreateProjectVm
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateOnly ProjectedStart { get; set; }
        public DateOnly ProjectedEnd { get; set; }
        public string? OwnerId { get; set; }
    }
}
