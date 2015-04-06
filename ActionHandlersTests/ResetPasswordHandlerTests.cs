using System;
using Action;
using ActionHandlers;
using Common.Tests.Builders.MockBuilders;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class ResetPasswordHandlerTests
    {
        protected MockRepositoryBuilder<User> RepositoryBuilder;
        protected MockPasswordHasherBuilder PasswordHasherBuilder;
        protected ResetPassword Action;
        protected User UserInDatabase;
        private string _originalPassword;

        [SetUp]
        public void Setup()
        {
            _originalPassword = "password";
            UserInDatabase = new User
            {
                ActivationKey = Guid.NewGuid(),
                Password = _originalPassword
            };
            Action = new ResetPassword(UserInDatabase);
            RepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(UserInDatabase)
                .WithUpdate();
            PasswordHasherBuilder = new MockPasswordHasherBuilder()
                .WithHashCreation();
        }

        private ResetPasswordHandler GetHandler()
        {
            return new ResetPasswordHandler(RepositoryBuilder.BuildObject(), PasswordHasherBuilder.BuildObject());
        }

        protected User PerformAction()
        {
            return GetHandler().Handle(Action);
        }
    }

    public class GivenUserHasResetTheirPassword : ResetPasswordHandlerTests
    {
        [Test]
        public void Then_the_user_key_should_reset()
        {
            PerformAction();

            Assert.AreEqual(new Guid(), UserInDatabase.ActivationKey);
        }

        [Test]
        public void Then_the_password_should_be_changed()
        {
            var originalPassword = "old";
            Action.ActionAgainst.Password = originalPassword;

            PerformAction();

            Assert.AreNotEqual(originalPassword, UserInDatabase.Password);
        }

        [Test]
        public void Then_the_password_should_be_hashed()
        {
            var passwordToHash = Action.ActionAgainst.Password;

            PerformAction();

            PasswordHasherBuilder.Mock.Verify(x => x.CreateHash(passwordToHash), Times.Once);
        }
    }
}
