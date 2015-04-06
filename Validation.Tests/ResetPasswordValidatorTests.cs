using System;
using System.Linq;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class ResetPasswordValidatorTests
    {
        private MockRepositoryBuilder<User> _repositoryBuilder;
        private User _user;
        private User _userInDatabase;

        [SetUp]
        public void Setup()
        {
            _userInDatabase = new User
            {
                Status = UserStatus.Active,
                ActivationKey = Guid.NewGuid()
            };
            _repositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(_userInDatabase);
            _user = new User
            {
                ActivationKey = _userInDatabase.ActivationKey,
                Password = "password"
            };
        }

        private ResetPasswordValidator GetValidator()
        {
            return new ResetPasswordValidator(_repositoryBuilder.BuildObject());
        }

        private FluentValidation.Results.ValidationResult PerformAction()
        {
            return GetValidator().Validate(_user);
        }

        [Test]
        public void When_everything_is_fine_it_should_return_no_errors()
        {
            var result = PerformAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void When_key_doesnt_match_it_should_return_an_error()
        {
            _user.ActivationKey = Guid.NewGuid();

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void When_password_is_not_supplied_then_error_should_be_returned()
        {
            _user.Password = String.Empty;

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.MissingPassword, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_password_is_less_than_7_characters_then_error_should_be_returned()
        {
            _user.Password = "123456";

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.PasswordTooShort, result.Errors.Single().ErrorMessage);
        }
    }
}
