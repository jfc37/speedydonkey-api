using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json.Serialization;

namespace SpeedyDonkeyApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "AccountApi",
                routeTemplate: "api/accounts/{userId}",
                defaults: new { controller = "AccountApi", userId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "api/users/{userId}",
                defaults: new { controller = "UserApi", userId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UserStudentApi",
                routeTemplate: "api/users/{userId}/students/{personId}",
                defaults: new { controller = "StudentApi", personId = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "StudentApi",
                routeTemplate: "api/students/{personId}",
                defaults: new { controller = "StudentApi", personId = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "StudentCourseApi",
                routeTemplate: "api/students/{personId}/courses/{courseId}",
                defaults: new { controller = "StudentApi", courseId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UserProfessorApi",
                routeTemplate: "api/users/{userId}/professors/{personId}",
                defaults: new { controller = "ProfessorApi", personId = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "ProfessorApi",
                routeTemplate: "api/professors/{personId}",
                defaults: new { controller = "ProfessorApi", personId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PersonCourseApi",
                routeTemplate: "api/professors/{personId}/courses/{courseId}",
                defaults: new { controller = "CourseApi", courseId = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "CourseApi",
                routeTemplate: "api/courses/{courseId}",
                defaults: new { controller = "CourseApi", courseId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "NoticeApi",
                routeTemplate: "api/courses/{courseId}/notices/{noticeId}",
                defaults: new { controller = "NoticeApi", noticeId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "LectureApi",
                routeTemplate: "api/courses/{courseId}/lectures/{lectureId}",
                defaults: new { controller = "LectureApi", lectureId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "LectureSearchApi",
                routeTemplate: "api/lectures",
                defaults: new { controller = "LectureApi" }
            );

            config.Routes.MapHttpRoute(
                name: "AssignmentApi",
                routeTemplate: "api/courses/{courseId}/assignments/{courseWorkId}",
                defaults: new { controller = "AssignmentApi", courseWorkId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AssignmentSearchApi",
                routeTemplate: "api/assignments",
                defaults: new { controller = "AssignmentApi" }
            );

            config.Routes.MapHttpRoute(
                name: "ExamApi",
                routeTemplate: "api/courses/{courseId}/exams/{courseWorkId}",
                defaults: new { controller = "ExamApi", courseWorkId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ExamSearchApi",
                routeTemplate: "api/exams",
                defaults: new { controller = "ExamApi" }
            );

            config.Routes.MapHttpRoute(
                name: "CourseGradeApi",
                routeTemplate: "api/students/{personId}/grades/courses/{courseId}",
                defaults: new { controller = "CourseGradeApi", courseId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CourseWorkGradeApi",
                routeTemplate: "api/students/{personId}/grades/courses/{courseId}/course_work/{courseWorkId}",
                defaults: new { controller = "CourseWorkGradeApi", courseWorkId = RouteParameter.Optional }
            );

            //Default to json
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            //Give json some camel casing
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new LowerCaseDelimitedPropertyNamesContractResovler('_');

            //Add support JSONP
            //var jsonpFormatter = new JsonpMediaTypeFormatter(jsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            //#if !DEBUG
            ////Force HTTPS on entire API
            //config.Filters.Add(new RequireHttpsAttribute());
            //#endif

            config.Services.Add(typeof(IExceptionLogger), new GlobalErrorLoggerService());


            //Specify values as appropriate (origins,headers,methods)
            var cors = new EnableCorsAttribute("http://spa-speedydonkey.azurewebsites.net,http://localhost:7300", "*", "GET, POST, PUT, DELETE");
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
