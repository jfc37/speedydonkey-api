using System.Net;
using System.Web.Http;

namespace SpeedyDonkeyApi.Extensions
{
    /// <summary>
    /// OK Negotiated Content Result with a ToString override that makes it nicer to log
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SpeedyDonkeyApi.Extensions.LoggableNegotiatedContentResult{T}" />
    public class OkLoggableNegotiatedContentResult<T> : LoggableNegotiatedContentResult<T>
    {
        public OkLoggableNegotiatedContentResult(T content, ApiController controller) : base(HttpStatusCode.OK, content, controller)
        {
        }
    }
}