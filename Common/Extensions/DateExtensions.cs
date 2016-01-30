using System;

namespace Common.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        private const int DaysInWeek = 7;

        public static bool IsUtc(this DateTimeOffset instance)
        {
            return instance.Date.Kind == DateTimeKind.Utc;
        }
        public static bool IsLocalTime(this DateTimeOffset instance)
        {
            return instance.Date.Kind == DateTimeKind.Local;
        }

        public static DateTimeOffset AddWeeks(this DateTimeOffset instance, int weeksToAdd)
        {
            return instance.AddDays(weeksToAdd * DaysInWeek);
        }

        public static bool IsBefore(this DateTimeOffset? instance, DateTimeOffset toCompare)
        {
            return instance.HasValue && instance.Value.IsBefore(toCompare);
        }

        public static bool IsOnOrBefore(this DateTimeOffset? instance, DateTimeOffset toCompare)
        {
            return instance.HasValue && instance.Value.IsOnOrBefore(toCompare);
        }

        public static bool IsBefore(this DateTimeOffset instance, DateTimeOffset toCompare)
        {
            return instance < toCompare;
        }

        public static bool IsOnOrBefore(this DateTimeOffset instance, DateTimeOffset toCompare)
        {
            return instance <= toCompare;
        }

        public static bool IsOnOrAfter(this DateTimeOffset instance, DateTimeOffset toCompare)
        {
            return instance >= toCompare;
        }

        public static bool IsBetween(this DateTimeOffset instance, DateTimeOffset start, DateTimeOffset end)
        {
            return instance.IsOnOrAfter(start) && instance.IsOnOrBefore(end);
        }

        public static bool IsOnOrAfter(this DateTimeOffset? instance, DateTimeOffset toCompare)
        {
            return instance.HasValue && instance.IsOnOrAfter(toCompare);
        }
        public static bool IsAfter(this DateTimeOffset? instance, DateTimeOffset toCompare)
        {
            return instance.HasValue && instance.Value.IsAfter(toCompare);
        }

        public static bool IsAfter(this DateTimeOffset instance, DateTimeOffset toCompare)
        {
            return instance > toCompare;
        }

        /// <summary>
        /// Moves the date into the past until it matches provided day
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime instance, DayOfWeek startOfWeek)
        {
            int diff = instance.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
    }

            return instance.AddDays(-1 * diff).Date;
        }
    }

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
