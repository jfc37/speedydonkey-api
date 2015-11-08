using System.Web.Http;
using System.Web.Mvc;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi
{
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            config.Filters.Add(new NullModelActionFilter());
            config.Filters.Add(new ValidateModelActionFilter());
            config.Filters.Add(new CurrentUserAttribute());
            config.Filters.Add(new BasicAuthAuthoriseAttribute());
        }
    }
}