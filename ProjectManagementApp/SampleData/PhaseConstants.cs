using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class Phases
    {
        public static Phase SimplePlanning { get; set; } = new Phase
        {
            Name = "Simple Planning",
            Description = "a complete phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
        };
        public static Phase SimpleSetup { get; set; } = new Phase
        {
            Name = "Simple Setup",
            Description = "a review phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
        };
        public static Phase SimpleDataEntry { get; set; } = new Phase
        {
            Name = "Simple Data Entry",
            Description = "an in progress phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
        };
        public static Phase SimpleQA { get; set; } = new Phase
        {
            Name = "Simple QA",
            Description = "a todo phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            Priority = Priority.High,
        };
        public static Phase SimpleNotifyCompletion { get; set; } = new Phase
        {
            Name = "Simple Notify Completion",
            Description = "another todo phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            Priority = Priority.Low,
        };
        public static Phase SimplePostAnalytics { get; set; } = new Phase
        {
            Name = "Simple Post Analytics",
            Description = "a backlog phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
        };
    }
}
