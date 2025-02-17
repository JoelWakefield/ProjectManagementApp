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
        private static DateOnly ChartEnd(DateOnly start, int duration) => start.AddDays(duration);
        public static int FromStart(this GanttItem item, DateOnly start) => item.Start > start ? (item.Start.ToDateTime(TimeOnly.MinValue) - start.ToDateTime(TimeOnly.MinValue)).Days : 0;
        public static int ToEnd(this GanttItem item, DateOnly start, int duration) => ChartEnd(start, duration) > item.End ? (ChartEnd(start, duration).ToDateTime(TimeOnly.MinValue) - item.End.ToDateTime(TimeOnly.MinValue)).Days : 0;
        public static bool OnChart(this GanttItem item, DateOnly start, int duration) => !(start > item.End || start.AddDays(duration) < item.Start);
        public static int Duration(this GanttItem item, DateOnly start, int duration)
        {
            var daysOnChart = duration - item.FromStart(start) - item.ToEnd(start, duration);
            return daysOnChart == 0 ? 1 : daysOnChart;
        }
    }
}
