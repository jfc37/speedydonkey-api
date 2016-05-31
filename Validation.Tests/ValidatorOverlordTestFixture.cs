using System.Linq;
using Actions;
using Autofac.Core.Registration;
using Common.Tests.Builders;
using FluentValidation;
using NUnit.Framework;
using Validation.Tests.Builders;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class ValidatorOverlordTestFixture
    {
        private ValidatorOverlordBuilder _validatorOverlordBuilder;
        private LifetimeScopeBuilder _lifetimeScopeBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _validatorOverlordBuilder = new ValidatorOverlordBuilder();
            _lifetimeScopeBuilder = new LifetimeScopeBuilder()
                .WithActionValidatorsRegistered();
        }

        private ValidatorOverlord BuildOverlord()
        {
            return _validatorOverlordBuilder
                .WithLifetimeScope(_lifetimeScopeBuilder.Build())
                .Build();
        }

        public class Validate : ValidatorOverlordTestFixture
        {
            private TestAction _actionToValidate;

            [SetUp]
            public void Setup()
            {
                _lifetimeScopeBuilder = new LifetimeScopeBuilder()
                    .WithActionValidatorsRegistered();
                _actionToValidate = new TestAction
                {
                    ActionAgainst = new TestObject()
                };
            }

            [Test]
            public void It_should_create_correct_validator()
            {
                _lifetimeScopeBuilder.WithActionValidatorsRegistered();

                var validatorOverlord = BuildOverlord();
                var validationResult = validatorOverlord.Validate<TestAction, TestObject>(_actionToValidate);

                Assert.AreEqual("test validator", validationResult.ValidationErrors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_return_no_validation_erros_when_validator_isnt_found()
            {
                _lifetimeScopeBuilder.WithNothingRegistered();

                var validatorOverlord = BuildOverlord();
                var validationResult = validatorOverlord.Validate<TestAction, TestObject>(_actionToValidate);

                Assert.IsTrue(validationResult.IsValid);
            }

            [Test]
            public void It_should_return_correct_error_mappings()
            {
                var validatorOverlord = BuildOverlord();
                var validationResult = validatorOverlord.Validate<TestAction, TestObject>(_actionToValidate);

                Assert.AreEqual("test validator", validationResult.ValidationErrors.Single().ErrorMessage);
                Assert.AreEqual("TestProperty", validationResult.ValidationErrors.Single().PropertyName);
            }

            [Test]
            public void It_should_return_error_when_object_validated_against_is_null()
            {
                _actionToValidate.ActionAgainst = null;

                var validatorOverlord = BuildOverlord();
                var validationResult = validatorOverlord.Validate<TestAction, TestObject>(_actionToValidate);

                Assert.AreEqual("ActionObject", validationResult.ValidationErrors.Single().PropertyName);
            }
        }

        internal class TestValidatorValidator :  AbstractValidator<TestObject>, IActionValidator<TestAction, TestObject>
        {
            public TestValidatorValidator()
            {
                RuleFor(x => x.TestProperty).NotEmpty().WithMessage("test validator");
            }
        }

        private class TestAction : SystemAction<TestObject>
        {
        }

        internal class TestObject
        {
            public string TestProperty { get; set; }
        }
    }
}
