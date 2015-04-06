using System;
using Action;
using ActionHandlers;
using Data.Tests.Builders;
using Models;
using Moq;
using Notification.Notifications;
using Notification.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class ForgottenPasswordHandlerTests
    {
        protected MockRepositoryBuilder<User> RepositoryBuilder;
        protected MockPostOfficeBuilder PostOfficeBuilder;
        protected ForgottenPassword Action;
        protected User UserInDatabase;

        [SetUp]
        public void Setup()
        {
            UserInDatabase = new User
            {
                Email = "blah"
            };
            Action = new ForgottenPassword(UserInDatabase);
            RepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(UserInDatabase)
                .WithUpdate();

            PostOfficeBuilder = new MockPostOfficeBuilder()
                .WithSending();
        }

        private ForgottenPasswordHandler GetHandler()
        {
            return new ForgottenPasswordHandler(RepositoryBuilder.BuildObject(), PostOfficeBuilder.BuildObject());
        }

        protected User PerformAction()
        {
            return GetHandler().Handle(Action);
        }
    }

    public class GivenUserHasForgottenTheirPassword : ForgottenPasswordHandlerTests
    {
        [Test]
        public void Then_the_user_key_should_be_updated()
        {
            PerformAction();

            Assert.AreNotEqual(new Guid(), UserInDatabase.ActivationKey);
        }

        [Test]
        public void Then_an_email_should_be_sent()
        {
            PerformAction();

            PostOfficeBuilder.Mock.Verify(x => x.Send(It.IsAny<UserForgotPassword>()), Times.Once);
        }
    }
}
