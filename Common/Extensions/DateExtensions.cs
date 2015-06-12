using System;

namespace Common.Extensions
{
    public static class DateExtensions
    {
        public static bool IsLessThan(this DateTime? instance, DateTime toCompare)
        {
            return instance.HasValue && instance.Value.IsLessThan(toCompare);
        }

        public static bool IsLessThan(this DateTime instance, DateTime toCompare)
        {
            return instance < toCompare;
        }
        public static bool IsGreaterThan(this DateTime? instance, DateTime toCompare)
        {
            return instance.HasValue && instance.Value.IsGreaterThan(toCompare);
        }

        public static bool IsGreaterThan(this DateTime instance, DateTime toCompare)
        {
            return instance > toCompare;
        }
    }
}
