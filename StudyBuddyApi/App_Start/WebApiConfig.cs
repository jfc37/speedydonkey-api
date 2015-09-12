using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "RegistrationApi",
                routeTemplate: "api/windy-lindy/registration/{registrationNumber}",
                defaults: new { controller = "RegistrationApi", registrationNumber = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ClassApi",
                routeTemplate: "api/classes/{id}",
                defaults: new { controller = "ClassApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PassStatisticApi",
                routeTemplate: "api/passes/statistics",
                defaults: new { controller = "PassStatisticApi" }
            );

            config.Routes.MapHttpRoute(
                name: "PassApi",
                routeTemplate: "api/passes/{id}",
                defaults: new { controller = "PassApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CurrentUserAnnouncementsApi",
                routeTemplate: "api/users/current/announcements",
                defaults: new { controller = "UserAnnouncementApi" }
                );

            config.Routes.MapHttpRoute(
                name: "CurrentUserPassPurchaseApi",
                routeTemplate: "api/users/current/passtemplates/{passTemplateId}",
                defaults: new { controller = "UserPassesApi" }
                );

            config.Routes.MapHttpRoute(
                name: "PassPurchaseApi",
                routeTemplate: "api/users/{userId}/passtemplates/{passTemplateId}",
                defaults: new { controller = "UserPassesApi" }
            );

            config.Routes.MapHttpRoute(
                name: "BookingApi",
                routeTemplate: "api/bookings/{id}",
                defaults: new { controller = "BookingApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CurrentUserEnroledBlocksApi",
                routeTemplate: "api/users/current/blocks",
                defaults: new { controller = "UserEnroledBlocksApi" }
            );

            config.Routes.MapHttpRoute(
                name: "CurrentUserPassesApi",
                routeTemplate: "api/users/current/passes",
                defaults: new { controller = "UserPassesApi" }
            );

            config.Routes.MapHttpRoute(
                name: "UserPassesApi",
                routeTemplate: "api/users/{id}/passes",
                defaults: new { controller = "UserPassesApi" }
            );


            config.Routes.MapHttpRoute(
                name: "ClassRegisterApi",
                routeTemplate: "api/classes/{id}/roll",
                defaults: new { controller = "ClassRollApi" }
            );

            config.Routes.MapHttpRoute(
                name: "ClassAttendanceApi",
                routeTemplate: "api/classes/{id}/attendance/{studentId}",
                defaults: new { controller = "ClassAttendanceApi", studentId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ClassPassStaticsticsApi",
                routeTemplate: "api/classes/{id}/passes/statistics",
                defaults: new { controller = "ClassPassStaticsticsApi" }
            );


            //Default to json
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            //Serialise enums to strings
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.Converters.Add(new StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings = jsonSetting;

            //Give json some camel casing
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new LowerCaseDelimitedPropertyNamesContractResovler('_');


            #if !DEBUG
            //Force HTTPS on entire API
            config.Filters.Add(new RequireHttpsAttribute());
            #endif

            config.Services.Add(typeof(IExceptionLogger), new GlobalErrorLoggerService());


            //Specify values as appropriate (origins,headers,methods)
            var websiteUrl = ConfigurationManager.AppSettings.Get("WebsiteUrl");
            websiteUrl = "https://" + websiteUrl;
            if (websiteUrl == "https://spa-speedydonkey.azurewebsites.net")
                websiteUrl = "https://spa-speedydonkey.azurewebsites.net,http://localhost:7300,http://localhost:3000";
            var cors = new EnableCorsAttribute(websiteUrl, "*", "*");
            config.EnableCors(cors);
        }
    }

    public class LowerCaseDelimitedPropertyNamesContractResovler : DefaultContractResolver
    {
        private readonly char _delimiter;

        public LowerCaseDelimitedPropertyNamesContractResovler(char delimiter)
            : base(true)
        {
            _delimiter = delimiter;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToDelimitedString(_delimiter);
        }
    }

    public static class StringExtensions
    {
        public static string ToDelimitedString(this string @string, char delimiter)
        {
            var camelCaseString = @string.ToCamelCaseString();
            return new string(InsertDelimiterBeforeCaps(camelCaseString, delimiter).ToArray());
        }

        public static string ToCamelCaseString(this string @string)
        {
            if (string.IsNullOrEmpty(@string) || !char.IsUpper(@string[0]))
            {
                return @string;
            }
            string lowerCasedFirstChar =
                char.ToLower(@string[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            if (@string.Length > 1)
            {
                lowerCasedFirstChar = lowerCasedFirstChar + @string.Substring(1);
            }
            return lowerCasedFirstChar;
        }

        private static IEnumerable<char> InsertDelimiterBeforeCaps(IEnumerable<char> input, char delimiter)
        {
            bool lastCharWasUppper = false;
            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    if (!lastCharWasUppper)
                    {
                        yield return delimiter;
                        lastCharWasUppper = true;
                    }
                    yield return char.ToLower(c);
                    continue;
                }

                yield return c;
                lastCharWasUppper = false;
            }
        }
    }
}
