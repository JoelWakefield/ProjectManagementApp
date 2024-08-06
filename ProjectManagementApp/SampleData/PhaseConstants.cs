using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class Phases
    {
        public static Phase SimplePlanning { get; set; } = new Phase
        {
            Name = "SimplePlanning",
            Description = "a complete phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.Complete.Id,
        };
        public static Phase SimpleSetup { get; set; } = new Phase
        {
            Name = "SimpleSetup",
            Description = "a review phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.Review.Id,
        };
        public static Phase SimpleDataEntry { get; set; } = new Phase
        {
            Name = "SimpleDataEntry",
            Description = "an in progress phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.InProgress.Id,
        };
        public static Phase SimpleQA { get; set; } = new Phase
        {
            Name = "SimpleQA",
            Description = "a todo phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            Priority = Priority.High,
            StageId = Stages.ToDo.Id,
        };
        public static Phase SimpleNotifyCompletion { get; set; } = new Phase
        {
            Name = "SimpleNotifyCompletion",
            Description = "another todo phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            Priority = Priority.Low,
            StageId = Stages.ToDo.Id,
        };
        public static Phase SimplePostAnalytics { get; set; } = new Phase
        {
            Name = "SimplePostAnalytics",
            Description = "a backlog phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.Backlog.Id,
        };
    }
}
