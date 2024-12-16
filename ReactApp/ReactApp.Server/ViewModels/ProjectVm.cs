namespace ReactApp.Server.ViewModels
{
    public class ProjectVm
    {
        public string? Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime ProjectedStart { get; set; }
        public DateTime ProjectedEnd { get; set; }
        public DateTime ActualStart { get; set; }
        public DateTime ActualEnd { get; set; }
        public float TotalWorkHours { get; set; }
        public string? OwnerName { get; set; }
    }
}
