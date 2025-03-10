namespace ReactApp.Server.ViewModels
{
    public class ProjectVm
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateOnly ProjectedStart { get; set; }
        public DateOnly ProjectedEnd { get; set; }
        public DateOnly ActualStart { get; set; }
        public DateOnly ActualEnd { get; set; }
        public float TotalWorkHours { get; set; }
        public string? OwnerName { get; set; }
    }
}
