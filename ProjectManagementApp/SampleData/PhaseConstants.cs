using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class PhaseData
    {
        public static Phase SimplePlanning() => new Phase
        {
            Name = "Simple Planning",
            Description = "a complete phase for simple project",
            ProjectId = "d837f053-fa04-4ba3-a6be-7ae458e0f6d6",
            StageId = "98cc21fc-0de8-475c-98e3-c56abdfb5d6b",
            OwnerId = "12f1d78d-3d64-4ea9-948b-2984ba574a68",
        };
        public static Phase SimpleSetup() => new Phase
        {
            Name = "Simple Setup",
            Description = "a review phase for simple project",
            ProjectId = "d837f053-fa04-4ba3-a6be-7ae458e0f6d6",
            StageId = "cdf4196d-8273-4701-8cd9-9137232a8fdb",
            OwnerId = "1eedf8e8-05de-47dd-80b1-f374b885b8cd",
        };
        public static Phase SimpleDataEntry() => new Phase
        {
            Name = "Simple Data Entry",
            Description = "an in progress phase for simple project",
            ProjectId = "d837f053-fa04-4ba3-a6be-7ae458e0f6d6",
            StageId = "81dfcfdd-ca04-4ff3-9658-19e1120f7201",
            OwnerId = "949e1119-9b7c-4dbf-8b10-a9fa842b67cb",
        };
        public static Phase SimpleQA() => new Phase
        {
            Name = "Simple QA",
            Description = "a todo phase for simple project",
            ProjectId = "d837f053-fa04-4ba3-a6be-7ae458e0f6d6",
            StageId = "0f161ea3-974e-41c1-be47-156ff9ce0543",
            OwnerId = "12f1d78d-3d64-4ea9-948b-2984ba574a68",
            Priority = Priority.High,
        };
        public static Phase SimpleNotifyCompletion() => new Phase
        {
            Name = "Simple Notify Completion",
            Description = "another todo phase for simple project",
            ProjectId = "d837f053-fa04-4ba3-a6be-7ae458e0f6d6",
            StageId = "c1d3f435-98ad-4530-b954-8f7ce9d528fe",
            OwnerId = "949e1119-9b7c-4dbf-8b10-a9fa842b67cb",
            Priority = Priority.Low,
        };
        public static Phase SimplePostAnalytics() => new Phase
        {
            Name = "Simple Post Analytics",
            Description = "a backlog phase for simple project",
            ProjectId = "d837f053-fa04-4ba3-a6be-7ae458e0f6d6",
            StageId = "0238eecd-edf5-4a58-a7ac-e78cb23109dd",
            OwnerId = "1eedf8e8-05de-47dd-80b1-f374b885b8cd",
        };
    }
}
