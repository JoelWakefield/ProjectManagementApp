using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class PhaseData
    {
        public static Phase SimplePlanning() => new Data.Phase
        {
            Name = "Simple Planning",
            Description = "a complete phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.Complete.Id,
            OwnerId = Users.Bert.Id,
        };
        public static Phase SimpleSetup { get; set; } = new Phase
        {
            Name = "Simple Setup",
            Description = "a review phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.Review.Id,
            OwnerId = Users.Zahir.Id,
        };
        public static Phase SimpleDataEntry { get; set; } = new Phase
        {
            Name = "Simple Data Entry",
            Description = "an in progress phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.InProgress.Id,
            OwnerId = Users.Alayah.Id,
        };
        public static Phase SimpleQA { get; set; } = new Phase
        {
            Name = "Simple QA",
            Description = "a todo phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.ToDo.Id,
            OwnerId = Users.Bert.Id,
            Priority = Priority.High,
        };
        public static Phase SimpleNotifyCompletion { get; set; } = new Phase
        {
            Name = "Simple Notify Completion",
            Description = "another todo phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.Backlog.Id,
            OwnerId = Users.Alayah.Id,
            Priority = Priority.Low,
        };
        public static Phase SimplePostAnalytics { get; set; } = new Phase
        {
            Name = "Simple Post Analytics",
            Description = "a backlog phase for simple project",
            ProjectId = Projects.SimpleProject.Id,
            StageId = Stages.Canceled.Id,
            OwnerId = Users.Zahir.Id,
        };
    }
}
