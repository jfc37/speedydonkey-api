using System.Linq;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class UpdatePassValidatorTests
    {
        private Pass _pass;
        private MockRepositoryBuilder<Pass> _passRepositoryBuilder;
        private Pass _savedPass;

        [SetUp]
        public void Setup()
        {
            _savedPass = new Pass();
            _passRepositoryBuilder = new MockRepositoryBuilder<Pass>().WithGet(_savedPass);
            _pass = new Pass();
        }

        private UpdatePassValidator GetValidator()
        {
            return new UpdatePassValidator(_passRepositoryBuilder.BuildObject());
        }

        private FluentValidation.Results.ValidationResult PerforAction()
        {
            return GetValidator().Validate(_pass);
        }

        [Test]
        public void When_it_is_valid_then_no_validation_errors_should_be_returned()
        {
            var result = PerforAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void When_the_pass_doesnt_exist_then_a_validation_error_should_be_returned()
        {
            _passRepositoryBuilder.WithUnsuccessfulGet();

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.InvalidPass, result.Errors.Single().ErrorMessage);
        }
    }
}
