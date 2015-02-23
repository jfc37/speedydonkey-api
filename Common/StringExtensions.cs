using System;
using System.ComponentModel;

namespace Common
{
    public static class StringExtensions
    {
        public static object ConvertToType(this string someString, Type returnType)
        {
            var typeConverter = TypeDescriptor.GetConverter(returnType);
            return typeConverter.ConvertFromInvariantString(someString);
        }
    }
}
