using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class CreateUpdatePassTemplateValidatorTests : ValidatorTests<CreateUpdatePassTemplateValidator, PassTemplate>
    {
        [SetUp]
        public void Setup()
        {
            Parameter = new PassTemplate
            {
                Description = "description",
                PassType = PassType.Clip.ToString()
            };
        }

        protected override CreateUpdatePassTemplateValidator GetValidator()
        {
            return new CreateUpdatePassTemplateValidator();
        }

        public class ThereIsNoValidationErrors : CreateUpdatePassTemplateValidatorTests
        {
            [Test]
            public void When_all_inputs_are_correct()
            {
                var result = PerformAction();

                Assert.IsTrue(result.IsValid);
            }
        }

        public class ThereIsAValidationError : CreateUpdatePassTemplateValidatorTests
        {

            [Test]
            public void When_description_is_missing()
            {
                Parameter.Description = "";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingDescription);
            }

            [Test]
            public void When_pass_type_is_missing()
            {
                Parameter.PassType = "";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingPassType);
            }

            [Test]
            public void When_pass_type_is_invalid()
            {
                Parameter.PassType = "bad";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidPassType);
            }

            [Test]
            public void When_cost_is_negative()
            {
                Parameter.Cost = -1;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.NegativeNumber);
            }

            [Test]
            public void When_weeks_valid_for_is_negative()
            {
                Parameter.WeeksValidFor = -1;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.NegativeNumber);
            }

            [Test]
            public void When_classes_valid_for_is_negative()
            {
                Parameter.ClassesValidFor = -1;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.NegativeNumber);
            }
        }
    }
}
