using System.Net.Http;
using System.Web.Http;
using ActionHandlersTests.Builders.MockBuilders;
using Actions;
using Models;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace StudyBuddyApi.Tests.Controllers
{
    [TestFixture]
    public class GenericApiControllerTests
    {
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockUrlConstructorBuilder _urlConstructorBuilder;

        private void DependencySetup<TAction, TEntity>() where TAction : IAction<TEntity>
        {
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder()
                .WithNoErrorsOnHandling<TAction, TEntity>();
            _urlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
        }

        private void SetupController(ApiController controller)
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }

        private void VerifyHandleOfAction<TAction, TEntity>() where TAction : IAction<TEntity>
        {
            _actionHandlerOverlordBuilder.Mock.Verify(x => x.HandleAction<TAction, TEntity>(It.IsAny<TAction>()), Times.Once);
        }

        public class GivenTheAccountApiIsCalled : GenericApiControllerTests
        {
            private AccountApiController GetController()
            {
                var controller = new AccountApiController(_actionHandlerOverlordBuilder.BuildObject(), _urlConstructorBuilder.BuildObject());
                SetupController(controller);
                return controller;
            }

            public class WhenItIsAPost : GivenTheAccountApiIsCalled
            {
                private AccountModel _model;

                [SetUp]
                public void Setup()
                {
                    DependencySetup<CreateAccount, Account>();
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
        }
    }
}
