using System;

namespace Common.Extensions.DateTimes
{
    public static class DisplayExtensions
    {
        /// <summary>
        /// Converts a date to a string which looks good in a url.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string ToUrlDateString(this DateTime instance)
        {
            return instance.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Converts a date to a string which looks good in a file name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string ToFileDateTimeString(this DateTime instance)
        {
            return instance.ToString("yyyyMMddHHmmss");
        }
    }
}