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

        public static List<T> PutIntoList<T>(this T instance)
        {
            return new List<T> {instance};
        }

        public static bool IsNull(this object instance)
        {
            return instance == null;
        }

        public static bool IsNotNull(this object instance)
        {
            return !instance.IsNull();
        }
    }
}
