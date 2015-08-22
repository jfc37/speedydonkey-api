using System;
using System.ComponentModel;

namespace Common.Extensions
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

        public static bool IsNullOrWhiteSpace(this string instance)
        {
            return String.IsNullOrWhiteSpace(instance);
        }

        public static bool IsNotNullOrWhiteSpace(this string instance)
        {
            return !instance.IsNullOrWhiteSpace();
        }

        public static bool IsSameAs(this string instance, string compare)
        {
            return instance.Equals(compare, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsNotSameAs(this string instance, string compare)
        {
            return !instance.IsSameAs(compare);
        }

        public static bool NotEquals(this object instance, object compare)
        {
            return !instance.Equals(compare);
        }

        public static bool IsInt(this string instance)
        {
            int output;
            return int.TryParse(instance, out output);
        }

        public static bool IsNotInt(this string instance)
        {
            return !instance.IsInt();
        }

        public static int ToInt(this string instance)
        {
            return int.Parse(instance);
        }

        public static Guid ToGuid(this string instance)
        {
            return Guid.Parse(instance);
        }
    }
}
