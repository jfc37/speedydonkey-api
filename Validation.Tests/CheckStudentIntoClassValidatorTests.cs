using System;
using System.Collections.Generic;
using System.Linq;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class CheckStudentIntoClassValidatorTests
    {
        private MockRepositoryBuilder<Class> _classRepositoryBuilder;
        private MockRepositoryBuilder<User> _userRepositoryBuilder;
        private Class _class;
        private User _retrievedUser;
        private Class _retrievedClass;

        private CheckStudentIntoClassValidator GetValidator()
        {
            return new CheckStudentIntoClassValidator(
                _userRepositoryBuilder.BuildObject(),
                _classRepositoryBuilder.BuildObject());
        }

        private FluentValidation.Results.ValidationResult PerforAction()
        {
            return GetValidator().Validate(_class);
        }

        [SetUp]
        public void Setup()
        {
            _retrievedClass = new Class
            {
                ActualStudents = new List<IUser>()
            };
            _classRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGet(_retrievedClass);
            _retrievedUser = new User
            {
                Passes = new List<IPass>
                {
                    new Pass
                    {
                        StartDate = DateTime.Now.AddDays(-1),
                        EndDate = DateTime.Now.AddDays(1),
                        PaymentStatus = PassPaymentStatus.Paid.ToString()
                    }
                }
            };
            _userRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(_retrievedUser);
            _class = new Class
            {
                Id = 12,
                ActualStudents = new List<IUser>
                {
                    new User()
                }
            };
        }

        [Test]
        public void When_everything_is_valid_it_should_return_no_errors()
        {
            var result = PerforAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void When_class_doesnt_exist_then_it_should_return_an_error()
        {
            _classRepositoryBuilder.WithUnsuccessfulGet();

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var error = result.Errors.Single();
            Assert.AreEqual(ValidationMessages.InvalidClass, error.ErrorMessage);
        }
        [Test]
        public void When_multiple_users_being_added_then_it_should_return_an_error()
        {
            _class.ActualStudents.Add(new User());

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var error = result.Errors.Single();
            Assert.AreEqual(ValidationMessages.IncorrectNumberOfAttendees, error.ErrorMessage);
        }
        [Test]
        public void When_user_doesnt_exist_then_it_should_return_an_error()
        {
            _userRepositoryBuilder.WithUnsuccessfulGet();

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var error = result.Errors.Single();
            Assert.AreEqual(ValidationMessages.InvalidUser, error.ErrorMessage);
        }
        [Test]
        public void When_user_doesnt_have_a_valid_pass_then_it_should_return_an_error()
        {
            _userRepositoryBuilder.WithSuccessfulGet();

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var error = result.Errors.Single();
            Assert.AreEqual(ValidationMessages.NoValidPasses, error.ErrorMessage);
        }
        [Test]
        public void When_user_doesnt_have_a_paid_for_pass_then_it_should_return_an_error()
        {
            _retrievedUser.Passes.Single().PaymentStatus = PassPaymentStatus.Pending.ToString();

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var error = result.Errors.Single();
            Assert.AreEqual(ValidationMessages.NoPaidForPasses, error.ErrorMessage);
        }
        [Test]
        public void When_user_is_already_attending_the_class_then_it_should_return_an_error()
        {
            _retrievedClass.ActualStudents.Add(_retrievedUser);

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var error = result.Errors.Single();
            Assert.AreEqual(ValidationMessages.AlreadyAttendingClass, error.ErrorMessage);
        }
    }
}
