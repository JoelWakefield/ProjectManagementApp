using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class Stages
    {
        public static Stage Backlog { get; set; } = new Stage { Name = "backlog", OrderId = 0 };
        public static Stage ToDo { get; set; } = new Stage { Name = "todo", OrderId = 1 };
        public static Stage InProgress { get; set; } = new Stage { Name = "inprogress", OrderId = 2 };
        public static Stage Review { get; set; } = new Stage { Name = "review", OrderId = 3 };
        public static Stage Complete { get; set; } = new Stage { Name = "complete", OrderId = 4 };
        public static Stage Canceled { get; set; } = new Stage { Name = "canceled", OrderId = 5 };
    }
}
