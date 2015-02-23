using System.Linq;

namespace Validation
{
    public static class FluentConverter
    {
        public static ValidationResult ToProjectValidationResult(FluentValidation.Results.ValidationResult results)
        {
            return new ValidationResult
            {
                ValidationErrors = results.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList()
            };
        }
    }
}
