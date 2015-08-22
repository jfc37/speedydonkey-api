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
                name: "CurrentUserApi",
                routeTemplate: "api/users/current",
                defaults: new { controller = "CurrentUserApi" }
            );

            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "api/users/{id}",
                defaults: new { controller = "UserApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UserNoteApi",
                routeTemplate: "api/users/{id}/notes",
                defaults: new { controller = "UserNoteApi" }
            );

            config.Routes.MapHttpRoute(
                name: "PassNoteApi",
                routeTemplate: "api/passes/{id}/notes",
                defaults: new { controller = "PassNoteApi" }
            );

            config.Routes.MapHttpRoute(
                name: "AnnouncementApi",
                routeTemplate: "api/announcements/{id}",
                defaults: new { controller = "AnnouncementApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "TeacherApi",
                routeTemplate: "api/teachers/{id}",
                defaults: new { controller = "TeacherApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UserActivationApi",
                routeTemplate: "api/users/activation/{id}",
                defaults: new { controller = "UserActivationApi" }
            );

            config.Routes.MapHttpRoute(
                name: "UserPasswordResetApi",
                routeTemplate: "api/users/password/reset/{id}",
                defaults: new { controller = "UserPasswordResetApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "LevelApi",
                routeTemplate: "api/levels/{id}",
                defaults: new { controller = "LevelApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "BlockApi",
                routeTemplate: "api/blocks/{id}",
                defaults: new { controller = "BlockApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GenerateAllBlocksApi",
                routeTemplate: "api/levels/all/blocks",
                defaults: new { controller = "BlockApi" }
            );

            config.Routes.MapHttpRoute(
                name: "LevelBlockApi",
                routeTemplate: "api/levels/{levelId}/blocks",
                defaults: new { controller = "BlockApi"}
            );

            config.Routes.MapHttpRoute(
                name: "EnrolmentApi",
                routeTemplate: "api/users/{id}/enrolment",
                defaults: new { controller = "EnrolmentApi" }
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
                name: "CurrentUserScheduleApi",
                routeTemplate: "api/users/current/schedules",
                defaults: new { controller = "UserScheduleApi" }
            );

            config.Routes.MapHttpRoute(
                name: "UserScheduleApi",
                routeTemplate: "api/users/{id}/schedules",
                defaults: new { controller = "UserScheduleApi" }
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
                name: "CurrentUserClaimsApi",
                routeTemplate: "api/users/current/claims",
                defaults: new { controller = "UserClaimsApi" }
            );

            config.Routes.MapHttpRoute(
                name: "UserClaimsApi",
                routeTemplate: "api/users/{id}/claims",
                defaults: new { controller = "UserClaimsApi" }
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

            config.Routes.MapHttpRoute(
                name: "ReferenceDataApi",
                routeTemplate: "api/reference/{id}",
                defaults: new { controller = "ReferenceDataApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PassTemplateApi",
                routeTemplate: "api/passtemplate/{id}",
                defaults: new { controller = "PassTemplateApi", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProfitReport",
                routeTemplate: "api/report/profit",
                defaults: new { controller = "ProfitReportApi" }
            );

            config.Routes.MapHttpRoute(
                name: "DatabaseApi",
                routeTemplate: "api/database",
                defaults: new { controller = "DatabaseApi" }
            );

            config.Routes.MapHttpRoute(
                name: "ActivityLogApi",
                routeTemplate: "api/activitylog",
                defaults: new { controller = "ActivityLogApi" }
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
