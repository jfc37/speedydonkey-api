using System.Linq;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Tests.Builders;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class CreateUserValidatorTestFixture
    {
        private CreateUserValidatorBuilder _createUserValidatorBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createUserValidatorBuilder = new CreateUserValidatorBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CreateUserValidator BuildCreateUserValidator()
        {
            return _createUserValidatorBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Validate : CreateUserValidatorTestFixture
        {
            private UserBuilder _userBuilder;

            [SetUp]
            public void Setup()
            {
                _contextBuilder.WithNoUsers();
                _userBuilder = new UserBuilder()
                    .WithValidInputs();
            }

            [Test]
            public void It_should_return_no_errors_when_user_is_valid()
            {
                _userBuilder.WithValidInputs();

                var createUserValidator = BuildCreateUserValidator();
                var results = createUserValidator.Validate(_userBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_username()
            {
                _userBuilder.WithUsername(null);

                var createUserValidator = BuildCreateUserValidator();
                var results = createUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Username", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingUsername, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_username_not_be_empty()
            {
                _userBuilder.WithUsername("");

                var createUserValidator = BuildCreateUserValidator();
                var results = createUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Username", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingUsername, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_username_be_unique()
            {
                const string username = "john";
                _contextBuilder.WithUser(new UserBuilder().WithUsername(username).Build());
                _userBuilder.WithUsername(username);

                var createUserValidator = BuildCreateUserValidator();
                var results = createUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Username", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.DuplicateUsername, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_password()
            {
                _userBuilder.WithPassword(null);

                var createUserValidator = BuildCreateUserValidator();
                var results = createUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Password", results.Errors.Single().PropertyName);
            }

            [Test]
            public void It_should_require_password_not_be_empty()
            {
                _userBuilder.WithPassword("");

                var createUserValidator = BuildCreateUserValidator();
                var results = createUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Password", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingPassword, results.Errors.Single().ErrorMessage);
            }
        }
    }
}
