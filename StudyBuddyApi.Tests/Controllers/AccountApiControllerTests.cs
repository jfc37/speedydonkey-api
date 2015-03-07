using Actions;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
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