using System.Web.Http;
using System.Web.Mvc;

namespace SpeedyDonkeyApi
{
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }
    }
}