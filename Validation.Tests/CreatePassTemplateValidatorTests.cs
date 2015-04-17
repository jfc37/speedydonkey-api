using System.Linq;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class CreatePassTemplateValidatorTests
    {
        private PassTemplate _passTemplate;

        [SetUp]
        public void Setup()
        {
            _passTemplate = new PassTemplate
            {
                Description = "description",
                PassType = PassType.Clip.ToString()
            };
        }

        private CreateUpdatePassTemplateValidator GetValidator()
        {
            return new CreateUpdatePassTemplateValidator();
        }

        private FluentValidation.Results.ValidationResult PerformAction()
        {
            return GetValidator().Validate(_passTemplate);
        }

        public class ThereIsNoValidationErrors : CreatePassTemplateValidatorTests
        {
            [Test]
            public void When_all_inputs_are_correct()
            {
                var result = PerformAction();

                Assert.IsTrue(result.IsValid);
            }
        }

        public class ThereIsAValidationError : CreatePassTemplateValidatorTests
        {
            private void ExpectValidationError(FluentValidation.Results.ValidationResult result, string expectedMessage)
            {
                Assert.IsFalse(result.IsValid);
                Assert.AreEqual(expectedMessage, result.Errors.Single().ErrorMessage);
            }

            [Test]
            public void When_description_is_missing()
            {
                _passTemplate.Description = "";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingDescription);
            }

            [Test]
            public void When_pass_type_is_missing()
            {
                _passTemplate.PassType = "";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingPassType);
            }

            [Test]
            public void When_pass_type_is_invalid()
            {
                _passTemplate.PassType = "bad";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidPassType);
            }

            [Test]
            public void When_cost_is_negative()
            {
                _passTemplate.Cost = -1;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.NegativeNumber);
            }

            [Test]
            public void When_weeks_valid_for_is_negative()
            {
                _passTemplate.WeeksValidFor = -1;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.NegativeNumber);
            }

            [Test]
            public void When_classes_valid_for_is_negative()
            {
                _passTemplate.ClassesValidFor = -1;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.NegativeNumber);
            }
        }


    }
}
