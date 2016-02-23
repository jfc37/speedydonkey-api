using System.Web.Http;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi
{
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new CurrentUserAttribute());
        }
    }
}