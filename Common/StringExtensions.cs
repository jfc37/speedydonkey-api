using System;
using System.ComponentModel;

namespace Common
{
    public static class StringExtensions
    {
        public static bool EqualsEnum<TEnum>(this string instance, TEnum toCompare) where TEnum : struct
        {
            return instance.Equals(toCompare.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static object ConvertToType(this string someString, Type returnType)
        {
            var typeConverter = TypeDescriptor.GetConverter(returnType);
            return typeConverter.ConvertFromInvariantString(someString);
        }

        public static string FormatWith(this string instance, params object[] parameters)
        {
            return String.Format(instance, parameters);
        }
    }
}
