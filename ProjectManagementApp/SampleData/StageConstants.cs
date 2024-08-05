using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
    public static class Stages
    {
        public static Stage Backlog { get; set; } = new Stage { Name = "backlog" };
        public static Stage ToDo { get; set; } = new Stage { Name = "todo" };
        public static Stage InProgress { get; set; } = new Stage { Name = "inprogress" };
        public static Stage Review { get; set; } = new Stage { Name = "review" };
        public static Stage Complete { get; set; } = new Stage { Name = "complete" };
        public static Stage Canceled { get; set; } = new Stage { Name = "canceled" };
    }
}
