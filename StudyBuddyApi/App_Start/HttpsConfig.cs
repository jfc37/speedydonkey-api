using System.Web.Http;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi
{
    public static class HttpsConfig
    {
        public static void Register(HttpConfiguration config)
        {
#if !DEBUG
            //Force HTTPS on entire API
            config.Filters.Add(new RequireHttpsAttribute());
#endif
        }
    }
}