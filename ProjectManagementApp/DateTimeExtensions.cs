namespace ProjectManagementApp
{
    public static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime date)
        {
            return new DateOnly(date.Year, date.Month, date.Day);
        }
        public static DateOnly ToDateOnly(this DateTime? date)
        {
            if (date == null)
            {
                return new DateOnly();
            } 
            else
            {
                return new DateOnly(date.Value.Year, date.Value.Month, date.Value.Day);
            }
        }
    }
}
