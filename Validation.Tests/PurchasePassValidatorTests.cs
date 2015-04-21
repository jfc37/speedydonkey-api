using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class PurchasePassValidatorTests
    {
        private User _user;
        private MockRepositoryBuilder<User> _userRepositoryBuilder;
        private ICurrentUser _currentUser;

        [SetUp]
        public void Setup()
        {
            _currentUser = new CurrentUser();
            _currentUser.Id = 1;
            _userRepositoryBuilder = new MockRepositoryBuilder<User>().WithSuccessfulGet();
            _user = new User
            {
                Id = 1,
                Passes = new List<IPass>
                {
                    new Pass
                    {
                        PaymentStatus = PassPaymentStatus.Pending.ToString()
                    }
                }
            };
        }

        private PurchasePassValidator GetValidator()
        {
            return new PurchasePassValidator(
                _userRepositoryBuilder.BuildObject(), _currentUser);
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
        public void When_user_doesnt_exist_then_error_should_be_returned()
        {
            _userRepositoryBuilder.WithUnsuccessfulGet();

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.InvalidUser, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_no_passes_are_provided_then_error_should_be_returned()
        {
            _user.Passes = null;

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.ProvidePasses, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_current_user_doesnt_have_permission_to_add_pass_for_another_then_error_should_be_returned()
        {
            _user.Id = 2;
            _userRepositoryBuilder.WithGet(new User {Claims = ""});

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.CannotAddPassForAnother, result.Errors.Single().ErrorMessage);
        }

        [Test]
        public void When_user_doesnt_have_permission_to_pass_payment_status_then_error_should_be_returned()
        {
            _user.Passes.Single().PaymentStatus = PassPaymentStatus.Paid.ToString();
            _userRepositoryBuilder.WithGet(new User {Claims = ""});

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(ValidationMessages.CannontAddPaidPass, result.Errors.Single().ErrorMessage);
        }
    }
}
