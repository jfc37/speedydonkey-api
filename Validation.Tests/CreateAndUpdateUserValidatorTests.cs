using System;
using System.Linq;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators.Users;

namespace Validation.Tests
{
    [TestFixture]
    public class CreateAndUpdateUserValidatorTests
    {
        private User _user;
        private MockRepositoryBuilder<User> _userRepositoryBuilder;
        
        [SetUp]
        public void Setup()
        {
            _userRepositoryBuilder = new MockRepositoryBuilder<User>().WithUnsuccessfulGet();
            _user = new User
            {
                Email = "email@email.com",
                Password = "12345678",
                Surname = "Chappers",
                FirstName = "Bob"
            };
        }

        private CreateUserValidator GetValidator()
        {
            return new CreateUserValidator(
                _userRepositoryBuilder.BuildObject());
        }

        private FluentValidation.Results.ValidationResult PerforAction()
        {
            return GetValidator().Validate(_user);
        }

        [Test]
        public void When_it_is_valid_then_no_validation_errors_should_be_returned()
        {
            var result = PerforAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void When_email_is_not_supplied_then_error_should_be_returned()
        {
            _user.Email = String.Empty;

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.MissingEmail, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_an_invalid_email_is_supplied_then_error_should_be_returned()
        {
            _user.Email = "email";

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.InvalidEmail, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_password_is_not_supplied_then_error_should_be_returned()
        {
            _user.Password = String.Empty;

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.MissingPassword, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_password_is_less_than_7_characters_then_error_should_be_returned()
        {
            _user.Password = "123456";

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.PasswordTooShort, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_first_name_is_not_supplied_then_error_should_be_returned()
        {
            _user.FirstName = String.Empty;

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.MissingFirstName, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_surname_is_not_supplied_then_error_should_be_returned()
        {
            _user.Surname = String.Empty;

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.MissingSurname, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_email_is_used_by_another_user_then_error_should_be_returned()
        {
            _user.Id = 10;
            _userRepositoryBuilder.WithGet(new User {Email = _user.Email, Id = 11});

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.DuplicateEmail, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_email_is_used_by_this_user_then_no_errors_should_be_returned()
        {
            _user.Id = 10;
            _userRepositoryBuilder.WithGet(new User {Email = _user.Email, Id = _user.Id});

            var result = PerforAction();

            Assert.IsTrue(result.IsValid);
        }
    }
}
