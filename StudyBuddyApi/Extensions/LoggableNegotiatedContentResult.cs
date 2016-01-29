using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Common.Extensions;

namespace SpeedyDonkeyApi.Extensions
{
    /// <summary>
    /// Negotiated Content Result with a ToString override that makes it nicer to log
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Web.Http.Results.NegotiatedContentResult{T}" />
    public class LoggableNegotiatedContentResult<T> : NegotiatedContentResult<T>
    {
        public LoggableNegotiatedContentResult(HttpStatusCode statusCode, T content, ApiController controller) : base(statusCode, content, controller)
        {
        }

        public override string ToString()
        {
            return this.ToDebugString(nameof(StatusCode), nameof(Content));
        }
    }
}