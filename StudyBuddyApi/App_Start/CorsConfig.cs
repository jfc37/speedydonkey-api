using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SpeedyDonkeyApi
{
    public static class CorsConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Specify values as appropriate (origins,headers,methods)
            var websiteUrl = ConfigurationManager.AppSettings.Get("WebsiteUrl");
            websiteUrl = "https://" + websiteUrl;
            if (websiteUrl == "https://spa-speedydonkey.azurewebsites.net")
                websiteUrl = "https://spa-speedydonkey.azurewebsites.net,http://localhost:7300,http://localhost:3000";
            var cors = new EnableCorsAttribute(websiteUrl, "*", "*");
            config.EnableCors(cors);
        }
    }
}