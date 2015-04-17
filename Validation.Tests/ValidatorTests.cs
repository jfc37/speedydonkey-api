using System.Linq;
using FluentValidation;
using NUnit.Framework;

namespace Validation.Tests
{
    [TestFixture]
    public abstract class ValidatorTests<TValidator, T> where TValidator : AbstractValidator<T>
    {
        protected T Parameter;

        protected FluentValidation.Results.ValidationResult PerformAction()
        {
            return GetValidator().Validate(Parameter);
        }

        protected abstract TValidator GetValidator();
        protected void ExpectValidationError(FluentValidation.Results.ValidationResult result, string expectedMessage)
        {
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(expectedMessage, result.Errors.Single().ErrorMessage);
        }
    }
}