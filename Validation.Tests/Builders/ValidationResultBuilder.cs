using System.Collections.Generic;

namespace Validation.Tests.Builders
{
    public class ValidationResultBuilder
    {
        private IList<ValidationError> _validationErrors;

        public ValidationResultBuilder()
        {
            _validationErrors = new List<ValidationError>();
        }

        public ValidationResult Build()
        {
            return new ValidationResult
            {
                ValidationErrors = _validationErrors
            };
        }

        public ValidationResultBuilder WithError()
        {
            _validationErrors.Add(new ValidationError("error", "error"));
            return this;
        }

        public ValidationResultBuilder WithNoErrors()
        {
            _validationErrors.Clear();
            return this;
        }
    }
}
