using System.Net.Http;
using System.Web.Http;
using ActionHandlersTests.Builders.MockBuilders;
using Actions;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace StudyBuddyApi.Tests.Controllers
{
    [TestFixture]
    public abstract class GenericApiControllerTests<TEntity> where TEntity : IEntity, new()
    {
        protected MockActionHandlerOverlordBuilder ActionHandlerOverlordBuilder;
        protected MockUrlConstructorBuilder UrlConstructorBuilder;
        protected MockRepositoryBuilder<TEntity> RepositoryBuilder; 

        protected void DependencySetup()
        {
            ActionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            UrlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
            RepositoryBuilder = new MockRepositoryBuilder<TEntity>()
                .WithSuccessfulGet();
        }

        protected void SetupActionHandler<TAction>() where TAction : IAction<TEntity>
        {
            ActionHandlerOverlordBuilder.WithNoErrorsOnHandling<TAction, TEntity>();
        }

        protected void SetupController(ApiController controller)
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }

        protected void VerifyHandleOfAction<TAction, TEntity>() where TAction : IAction<TEntity>
        {
            ActionHandlerOverlordBuilder.Mock.Verify(x => x.HandleAction<TAction, TEntity>(It.IsAny<TAction>()), Times.Once);
        }

        protected void VerifyGetByIdCalled(int id)
        {
            RepositoryBuilder.Mock.Verify(x => x.Get(id), Times.Once);
        }
        protected void VerifyGetAllCalled()
        {
            RepositoryBuilder.Mock.Verify(x => x.GetAll(), Times.Once);
        }
    }
    public class GivenTheAccountApiIsCalled : GenericApiControllerTests<Account>
    {
        private AccountApiController GetController()
        {
            var controller = new AccountApiController(
                ActionHandlerOverlordBuilder.BuildObject(), 
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject());
            SetupController(controller);
            return controller;
        }

        public class WhenItIsAPost : GivenTheAccountApiIsCalled
        {
            private AccountModel _model;

            [SetUp]
            public void Setup()
            {
                DependencySetup();
                SetupActionHandler<CreateAccount>();
                _model = new AccountModel();
            }

            private void PerformAction()
            {
                GetController().Post(_model);
            }

            [Test]
            public void It_should_use_create_account_action()
            {
                PerformAction();
                VerifyHandleOfAction<CreateAccount, Account>();
            }
        }

        public class WhenItIsAGetById : GivenTheAccountApiIsCalled
        {
            private int _id;

            [SetUp]
            public void Setup()
            {
                DependencySetup();
            }

            private void PerformAction()
            {
                GetController().Get(_id);
            }

            [Test]
            public void It_should_get_account_by_id()
            {
                PerformAction();

                VerifyGetByIdCalled(_id);
            }
        }

        public class WhenItIsAGet : GivenTheAccountApiIsCalled
        {
            [SetUp]
            public void Setup()
            {
                DependencySetup();
            }

            private void PerformAction()
            {
                GetController().Get();
            }

            [Test]
            public void It_should_get_all_accounts()
            {
                PerformAction();

                VerifyGetAllCalled();
            }
        }
    }
}
