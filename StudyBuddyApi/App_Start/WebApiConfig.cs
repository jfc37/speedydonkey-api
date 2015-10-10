using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi
{
    public static class WebApiConfig
    {
        private static RaygunWebApiClient _client;
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();


            //Default to json
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            //Serialise enums to strings
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;

            //Camel case json
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            #if !DEBUG
            //Force HTTPS on entire API
            config.Filters.Add(new RequireHttpsAttribute());
            #endif

            var raygunApiKey = ConfigurationManager.AppSettings.Get("RaygunKey");
            RaygunWebApiClient.Attach(config, () =>
            {
                if (_client == null)
                {
                    _client = new RaygunWebApiClient(raygunApiKey);
                    _client.SendingMessage += client_SendingMessage;
                }

                return _client;
            });

            //Specify values as appropriate (origins,headers,methods)
            var websiteUrl = ConfigurationManager.AppSettings.Get("WebsiteUrl");
            websiteUrl = "https://" + websiteUrl;
            if (websiteUrl == "https://spa-speedydonkey.azurewebsites.net")
                websiteUrl = "https://spa-speedydonkey.azurewebsites.net,http://localhost:7300,http://localhost:3000";
            var cors = new EnableCorsAttribute(websiteUrl, "*", "*");
            config.EnableCors(cors);
        }

        private static void client_SendingMessage(object sender, RaygunSendingMessageEventArgs e)
        {
            if (e.Message.Details.Request == null || e.Message.Details.Request.Url == null || e.Message.Details.Response == null)
            {
                return;
            }
            var code = e.Message.Details.Response.StatusCode;

            if (code == 404 || code == 400)
            {
                e.Cancel = true;
            }
        }
    }
}
