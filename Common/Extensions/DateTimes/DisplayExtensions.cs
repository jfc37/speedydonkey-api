using System;

namespace Common.Extensions.DateTimes
{
    public static class DisplayExtensions
    {
        /// <summary>
        /// Converts a date to a string which looks good in a url
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string ToUrlDateString(this DateTime instance)
        {
            return instance.ToString("yyyy-MM-dd");
        }
    }
}