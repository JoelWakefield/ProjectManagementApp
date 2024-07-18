using ProjectManagementApp.Data;

namespace ProjectManagementApp.Models
{
    public static class ProjectConstants
    {
        public static Project SimpleProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "SimpleProject",
            Description = "A Project with one worker and one phase.",
            ProjectedStart = DateTime.UtcNow.AddDays(10),
            ProjectedEnd = DateTime.UtcNow.AddDays(14),
        };
        public static Project MultiPhaseProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "SimpleProject",
            Description = "A Project with one worker and multiple phases.",
            ProjectedStart = DateTime.UtcNow.AddDays(12),
            ProjectedEnd = DateTime.UtcNow.AddDays(22),
        };
        public static Project MultiPersonProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "SimpleProject",
            Description = "A Project with multiple workers, and one phase.",
            ProjectedStart = DateTime.UtcNow.AddDays(13),
            ProjectedEnd = DateTime.UtcNow.AddDays(14),
        };
        public static Project MultiPersonMultiPhaseProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "SimpleProject",
            Description = "A Project with multiple workers, and multiple phases.",
            ProjectedStart = DateTime.UtcNow.AddDays(16),
            ProjectedEnd = DateTime.UtcNow.AddDays(30),
        };
    }
}
