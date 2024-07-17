namespace ProjectManagementApp.Data
{
    public class Project
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ProjectedStart { get; set; }
        public DateTime ProjectedEnd { get; set; }
        public DateTime ActualStart { get; set; }
        public DateTime ActualEnd { get; set; }
        public float TotalWorkHours { get; set; }

        public Project(string name, string userId)
        {
            Id = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
            CreatedBy = userId;
            Name = name;
        }
    }
}
