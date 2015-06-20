using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object instance)
        {
            return JsonConvert.SerializeObject(instance);
        }

        public static List<T> ToList<T>(this T instance)
        {
            return new List<T> {instance};
        }
    }
}
