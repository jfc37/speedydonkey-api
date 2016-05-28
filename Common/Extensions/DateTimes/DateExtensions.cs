using System;

namespace Common.Extensions.DateTimes
{
    public static class DateExtensions
    {
        private const int DaysInWeek = 7;

        public static bool IsBefore(this DateTime? instance, DateTime toCompare)
        {
            return instance.HasValue && instance.Value.IsBefore(toCompare);
        }

        public static bool IsBefore(this DateTime instance, DateTime toCompare)
        {
            return instance < toCompare;
        }

        public static bool IsOnOrBefore(this DateTime instance, DateTime toCompare)
        {
            return instance <= toCompare;
        }

        public static bool IsOnOrAfter(this DateTime instance, DateTime toCompare)
        {
            return instance >= toCompare;
        }
        public static bool IsAfter(this DateTime? instance, DateTime toCompare)
        {
            return instance.HasValue && instance.Value.IsAfter(toCompare);
        }

        public static bool IsAfter(this DateTime instance, DateTime toCompare)
        {
            return instance > toCompare;
        }

        public static DateTime AddWeeks(this DateTime instance, int weeksToAdd)
        {
            return instance.AddDays(weeksToAdd * DaysInWeek);
        }
    }
}