using System;

namespace Common.Extensions
{
    public static class DateExtensions
    {
        private const int DaysInWeek = 7;

        public static bool IsLessThan(this DateTime? instance, DateTime toCompare)
        {
            return instance.HasValue && instance.Value.IsLessThan(toCompare);
        }

        public static bool IsLessThan(this DateTime instance, DateTime toCompare)
        {
            return instance < toCompare;
        }

        public static bool IsLessThanOrEqualTo(this DateTime instance, DateTime toCompare)
        {
            return instance <= toCompare;
        }
        public static bool IsGreaterThan(this DateTime? instance, DateTime toCompare)
        {
            return instance.HasValue && instance.Value.IsGreaterThan(toCompare);
        }

        public static bool IsGreaterThan(this DateTime instance, DateTime toCompare)
        {
            return instance > toCompare;
        }

        public static DateTime AddWeeks(this DateTime instance, int weeksToAdd)
        {
            return instance.AddDays(weeksToAdd * DaysInWeek);
        }
    }
}
