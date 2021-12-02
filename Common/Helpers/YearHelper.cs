namespace Common.Helpers
{
    public sealed class YearHelper
    {
        public static YearHelper Instance { get; } = new();

        private int? _year;

        static YearHelper()
        {
        }

        private YearHelper()
        {
        }

        public int GetAoCYear()
        {
            if (_year == null) _year = CalculateAocYear();
            return (int)_year;
        }

        private int CalculateAocYear()
        {
            var date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").Date;
            return date.Month == 12? date.Year : date.Year - 1;
        }
    }
}