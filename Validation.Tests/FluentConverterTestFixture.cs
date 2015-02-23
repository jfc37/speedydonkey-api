using System.Linq;
using FluentValidation.Results;
using NUnit.Framework;

namespace Validation.Tests
{
    [TestFixture]
    public class FluentConverterTestFixture
    {
        public class ToProjectValidationResult : FluentConverterTestFixture
        {
            [Test]
            public void It_should_return_empty_list_when_no_errors()
            {
                var  fluentValidationResult = new FluentValidation.Results.ValidationResult();

                var validationErrors = FluentConverter.ToProjectValidationResult(fluentValidationResult);

                Assert.IsEmpty(validationErrors.ValidationErrors);
            }

            [Test]
            public void It_should_map_error_property()
            {
                var fluentValidationResult = new FluentValidation.Results.ValidationResult();
                fluentValidationResult.Errors.Add(new ValidationFailure("property", "error"));

                var validationErrors = FluentConverter.ToProjectValidationResult(fluentValidationResult);

                Assert.AreEqual(fluentValidationResult.Errors.Single().PropertyName, validationErrors.ValidationErrors.Single().PropertyName);
            }

            [Test]
            public void It_should_map_error_message()
            {
                var fluentValidationResult = new FluentValidation.Results.ValidationResult();
                fluentValidationResult.Errors.Add(new ValidationFailure("property", "error"));

                var validationErrors = FluentConverter.ToProjectValidationResult(fluentValidationResult);

                Assert.AreEqual(fluentValidationResult.Errors.Single().ErrorMessage, validationErrors.ValidationErrors.Single().ErrorMessage);
            }
        }
    }
}
