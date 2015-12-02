using System.Web.Http;

namespace SpeedyDonkeyApi
{
    public static class RouteConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}