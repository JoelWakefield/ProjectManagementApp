namespace ReactApp.Server
{
    public static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime date)
        {
            return new DateOnly(date.Year, date.Month, date.Day);
        }
    }
}
