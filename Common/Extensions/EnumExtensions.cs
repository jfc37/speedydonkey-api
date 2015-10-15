using System;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static T Parse<T>(this string instance)
        {
            return (T) Enum.Parse(typeof (T), instance);
        }
    }
}