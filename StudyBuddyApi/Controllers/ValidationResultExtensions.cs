using System.Net;
using Validation;

namespace SpeedyDonkeyApi.Controllers
{
    public static class ValidationResultExtensions
    {
        public static HttpStatusCode GetStatusCode(this ValidationResult instance, HttpStatusCode validStatusCode)
        {
            return instance.IsValid
                ? validStatusCode
                : HttpStatusCode.BadRequest;
        }
    }
}