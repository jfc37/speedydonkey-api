using System.Linq;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;
using Validation.Tests.Builders;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class UpdateUserValidatorTestFixture
    {
        private UpdateUserValidatorBuilder _updateUserValidatorBuilder;
        private MockUserRepositoryBuilder _userRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateUserValidatorBuilder = new UpdateUserValidatorBuilder();
            _userRepositoryBuilder = new MockUserRepositoryBuilder();
        }

        private UpdateUserValidator BuildUpdateUserValidator()
        {
            return _updateUserValidatorBuilder
                .WithUserRepository(_userRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Validate : UpdateUserValidatorTestFixture
        {
            private UserBuilder _userBuilder;

            [SetUp]
            public void Setup()
            {
                _userBuilder = new UserBuilder()
                    .WithValidInputs();
                _userRepositoryBuilder.WithUser(_userBuilder.Build());
            }

            [Test]
            public void It_should_return_no_errors_when_user_is_valid()
            {
                _userBuilder.WithValidInputs();
                _userRepositoryBuilder.WithUser(_userBuilder.Build());

                var updateUserValidator = BuildUpdateUserValidator();
                var results = updateUserValidator.Validate(_userBuilder.Build());

                Assert.IsTrue(results.IsValid);
            }

            [Test]
            public void It_should_require_user_be_existing()
            {
                _userBuilder.WithId(1);
                _userRepositoryBuilder
                    .WithNoUsers();

                var updateUserValidator = BuildUpdateUserValidator();
                var results = updateUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Id", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.UserDoesntExist, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_username_not_be_empty()
            {
                _userBuilder.WithUsername("");

                var updateUserValidator = BuildUpdateUserValidator();
                var results = updateUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Username", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingUsername, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_username_be_unique()
            {
                const string username = "john";
                _userRepositoryBuilder.WithUser(new UserBuilder().WithId(2).WithUsername(username).Build());
                _userBuilder.WithUsername(username);

                var updateUserValidator = BuildUpdateUserValidator();
                var results = updateUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Username", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.DuplicateUsername, results.Errors.Single().ErrorMessage);
            }

            [Test]
            public void It_should_require_password()
            {
                _userBuilder.WithPassword(null);

                var updateUserValidator = BuildUpdateUserValidator();
                var results = updateUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Password", results.Errors.Single().PropertyName);
            }

            [Test]
            public void It_should_require_password_not_be_empty()
            {
                _userBuilder.WithPassword("");

                var updateUserValidator = BuildUpdateUserValidator();
                var results = updateUserValidator.Validate(_userBuilder.Build());

                Assert.AreEqual("Password", results.Errors.Single().PropertyName);
                Assert.AreEqual(ValidationMessages.MissingPassword, results.Errors.Single().ErrorMessage);
            }
        }
    }
}
