using ProjectManagementApp.Data;

namespace ProjectManagementApp.ViewModels
{
    public class GanttItem
    {
        public GanttItem(PhaseSchedule schedule, string name)
        {
            PhaseId = schedule.PhaseId;
            Name = name;
            Start = DateOnly.FromDateTime(schedule.Start);
            End = DateOnly.FromDateTime(schedule.End);
        }

        public string PhaseId { get; set; }
        public string Name { get; set; }
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }
    }

    public static class GanttItemExtensions
    {
        private static DateOnly Today { get => DateOnly.FromDateTime(DateTime.UtcNow); }
        private static DateOnly ChartEnd(int daysOnChart) => Today.AddDays(daysOnChart);
        public static int FromStart(this GanttItem item) => item.Start > Today ? (item.Start.ToDateTime(TimeOnly.MinValue) - Today.ToDateTime(TimeOnly.MinValue)).Days : 0;
        public static int ToEnd(this GanttItem item, int daysOnChart) => ChartEnd(daysOnChart) > item.End ? (ChartEnd(daysOnChart).ToDateTime(TimeOnly.MinValue) - item.End.ToDateTime(TimeOnly.MinValue)).Days : 0;
        public static bool OnChart(this GanttItem item, int daysOnChart) => !(Today > item.End || ChartEnd(daysOnChart) < item.Start);
        public static int Duration(this GanttItem item, int daysOnChart)
        {
            var duration = daysOnChart - item.FromStart() - item.ToEnd(daysOnChart);
            return duration == 0 ? 1 : duration;
        }
    }
}
