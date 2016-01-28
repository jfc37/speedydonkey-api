using System.Net;
using System.Web.Http;

namespace SpeedyDonkeyApi.Extensions
{
    public static class ApiControllerExtensions
    {
        public static IHttpActionResult BadRequestWithContent<T>(this ApiController instance, T content)
        {
            return new LoggableNegotiatedContentResult<T>(HttpStatusCode.BadRequest, content, instance);
        }

        public static IHttpActionResult CreatedWithContent<T>(this ApiController instance, T content)
        {
            return new LoggableNegotiatedContentResult<T>(HttpStatusCode.Created, content, instance);
        }

        public static IHttpActionResult OkWithContent<T>(this ApiController instance, T content)
        {
            return new LoggableNegotiatedContentResult<T>(HttpStatusCode.OK, content, instance);
        }
    }
}