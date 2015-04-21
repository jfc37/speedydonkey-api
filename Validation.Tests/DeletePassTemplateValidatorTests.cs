using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    public class DeletePassTemplateValidatorTests : ValidatorTests<DeletePassTemplateValidator, PassTemplate>
    {
        protected MockRepositoryBuilder<PassTemplate> RepositoryBuilder; 
        protected override DeletePassTemplateValidator GetValidator()
        {
            return new DeletePassTemplateValidator(RepositoryBuilder.BuildObject());
        }

        [SetUp]
        public void Setup()
        {
            Parameter = new PassTemplate{Id = 1};

            RepositoryBuilder = new MockRepositoryBuilder<PassTemplate>()
                .WithGet(new PassTemplate{Id = 1});
        }
        public class ThereIsNoValidationErrors : DeletePassTemplateValidatorTests
        {
            [Test]
            public void When_all_inputs_are_correct()
            {
                var result = PerformAction();

                Assert.IsTrue(result.IsValid);
            }
        }

        public class ThereIsAValidationError : DeletePassTemplateValidatorTests
        {

            [Test]
            public void When_pass_template_doesnt_exist()
            {
                RepositoryBuilder = new MockRepositoryBuilder<PassTemplate>()
                    .WithUnsuccessfulGet();

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidItemToDelete);
            }
        }
    }
}
