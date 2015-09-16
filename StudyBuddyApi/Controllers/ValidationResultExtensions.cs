using System.Net;
using System.Web.Http.ModelBinding;
using Validation;

namespace SpeedyDonkeyApi.Controllers
{
    public static class ValidationResultExtensions
    {
        public static ModelStateDictionary ToModelState(this ValidationResult instance)
        {
            var modelState = new ModelStateDictionary();
            foreach (var validationError in instance.ValidationErrors)
            {
                modelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
            }

            return modelState;
        }

        public static HttpStatusCode GetStatusCode(this ValidationResult instance, HttpStatusCode validStatusCode)
        {
            return instance.IsValid
                ? validStatusCode
                : HttpStatusCode.BadRequest;
        }
    }
}