using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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

        public static bool DoesNotContain(this string instance, string value)
        {
            return !instance.Contains(value);
        }

        public static int ToInt(this string instance)
        {
            return int.Parse(instance);
        }

        public static Guid ToGuid(this string instance)
        {
            return Guid.Parse(instance);
        }

        /// <summary>
        /// Nicely formats a string description of the object included the requested properties
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns></returns>
        public static string ToDebugString(this object instance, params string[] propertyNames)
        {
            var instanceType = instance.GetType();

            var stringList = new List<string>();
            foreach (var propertyName in propertyNames)
            {
                var propertyValue = instanceType.GetProperty(propertyName).GetValue(instance);

                if (propertyValue.IsOfType<IEnumerable>() && propertyValue.IsNotOfType<string>())
                {
                    propertyValue = ((IEnumerable) propertyValue).ToCollectionDebugString();
                }

                stringList.Add($"{propertyName}: {propertyValue}");
                
            }

            return "{" +
                   $"{instanceType.Name}: " +
                   $"{stringList.JoinToString(", ")}" +
                   "}";
        }

        private static string ToCollectionDebugString(this IEnumerable instance)
        {
            var index = 1;
            return "[" +
                   $"{(from object item in instance select $"{index++}: {item}").ToList().JoinToString(", ")}" +
                   "]";
        }
    }
}
