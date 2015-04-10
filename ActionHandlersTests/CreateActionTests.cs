using System;
using ActionHandlers.CreateHandlers;
using Actions;
using Common;
using Common.Tests.Builders.MockBuilders;
using Data.Tests.Builders;
using Data.Tests.Builders.MockBuilders;
using Models;
using Moq;
using Notification;
using Notification.Notifications;
using Notification.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public abstract class CreateActionTests<TEntity> where TEntity : class, IEntity, new()
    {
        protected MockRepositoryBuilder<TEntity> RepositoryBuilder;
        protected MockAppSettingsBuilder AppSettingsBuilder;

        protected void SetupDependencies()
        {
            RepositoryBuilder = new MockRepositoryBuilder<TEntity>()
                .WithCreate();

            AppSettingsBuilder = new MockAppSettingsBuilder()
                .WithAllSettings();
        }

        protected void CheckCreateWasCalled()
        {
            RepositoryBuilder.Mock.Verify(x => x.Create(It.IsAny<TEntity>()), Times.Once);
        }
    }

    public class GivenCreateUserIsHandled : CreateActionTests<User>
    {
        private MockPasswordHasherBuilder _passwordHasherBuilder;
        private MockPostOfficeBuilder _postOfficeBuilder;
        private CreateUser _action;

        [SetUp]
        public void Setup()
        {
            SetupDependencies();
            _passwordHasherBuilder = new MockPasswordHasherBuilder()
                .WithHashCreation();
            _postOfficeBuilder = new MockPostOfficeBuilder()
                .WithSending();
            _action = new CreateUser(new User{Email = "saran@fullswing.co.nz"});
        }

        private CreateUserHandler GetHandler()
        {
            return new CreateUserHandler(RepositoryBuilder.BuildObject(), _passwordHasherBuilder.BuildObject(), _postOfficeBuilder.BuildObject(), AppSettingsBuilder.BuildObject());
        }

        private void PerformAction()
        {
            GetHandler().Handle(_action);
        }

        [Test]
        public void It_should_hash_the_password()
        {
            PerformAction();

            _passwordHasherBuilder.Mock.Verify(x => x.CreateHash(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void It_should_call_create_on_repository()
        {
            PerformAction();

            CheckCreateWasCalled();
        }

        [Test]
        public void It_should_send_confirmation_email()
        {
            PerformAction();

            _postOfficeBuilder.Mock.Verify(x => x.Send(It.IsAny<UserRegistered>()), Times.Once);
        }

        public class WhenEmailIsAnAdminEmailAddress : GivenCreateUserIsHandled
        {

            [Test]
            public void Then_the_user_should_have_special_claims()
            {
                _action.ActionAgainst.Email = "admin@email.com";
                AppSettingsBuilder.WithSetting(AppSettingKey.AdminEmailWhitelist, String.Format("|{0}|", _action.ActionAgainst.Email));
                AppSettingsBuilder.WithSetting(AppSettingKey.AutoActivateAdmin, "true");

                PerformAction();

                Assert.IsNotNullOrEmpty(_action.ActionAgainst.Claims);
            }
        }

        public class WhenEmailIsNotAnAdminEmailAddress : GivenCreateUserIsHandled
        {

            [Test]
            public void Then_the_user_should_have_no_special_claims()
            {
                _action.ActionAgainst.Email = "someemail@email.com";
                AppSettingsBuilder.WithSetting(AppSettingKey.AdminEmailWhitelist, "|someadmin@email.com|");

                PerformAction();

                Assert.IsNullOrEmpty(_action.ActionAgainst.Claims);
            }
        }
    }
}
