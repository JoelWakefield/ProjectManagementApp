using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class Projects
    {
        public static Project SimpleProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "Simple Project",
            Description = "A Project with one worker and one phase.",
            ProjectedStart = DateTime.UtcNow.AddDays(10),
            ProjectedEnd = DateTime.UtcNow.AddDays(14),
            OwnerId = Users.Mylo.Id,
            Phases = new List<Phase>
            {
                PhaseData.SimplePlanning(),
                PhaseData.SimpleSetup(),
                PhaseData.SimpleDataEntry(),
                PhaseData.SimpleQA(),
                PhaseData.SimpleNotifyCompletion(),
                PhaseData.SimplePostAnalytics(),
            }
        };
        public static Project MultiPhaseProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "Multi Phase Project",
            Description = "A Project with one worker and multiple phases.",
            ProjectedStart = DateTime.UtcNow.AddDays(12),
            ProjectedEnd = DateTime.UtcNow.AddDays(22),
            OwnerId = Users.Mylo.Id,
        };
        public static Project MultiPersonProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "Multi Worker Project",
            Description = "A Project with multiple workers, and one phase.",
            ProjectedStart = DateTime.UtcNow.AddDays(13),
            ProjectedEnd = DateTime.UtcNow.AddDays(14),
            OwnerId = Users.Alayah.Id,
        };
        public static Project MultiPersonMultiPhaseProject { get; set; } = new Project()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedBy = "project_setup",
            CreatedOn = DateTime.UtcNow,
            Name = "Multi Worker Multi Phase Project",
            Description = "A Project with multiple workers, and multiple phases.",
            ProjectedStart = DateTime.UtcNow.AddDays(16),
            ProjectedEnd = DateTime.UtcNow.AddDays(30),
            OwnerId = Users.Alayah.Id,
        };
    }
}
