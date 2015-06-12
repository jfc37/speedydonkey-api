using Newtonsoft.Json;

namespace Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object instance)
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}
