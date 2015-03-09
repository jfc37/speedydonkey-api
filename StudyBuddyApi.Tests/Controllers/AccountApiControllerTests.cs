using Actions;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    public class GivenTheUserApiIsCalled : GenericApiControllerTests<User>
    {
        private UserApiController GetController()
        {
            var controller = new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(), 
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject());
            SetupController(controller);
            return controller;
        }

        public class WhenItIsAPost : GivenTheUserApiIsCalled
        {
            private UserModel _model;

            [SetUp]
            public void Setup()
            {
                DependencySetup();
                SetupActionHandler<CreateUser>();
                _model = new UserModel();
            }

            private void PerformAction()
            {
                GetController().Post(_model);
            }

            [Test]
            public void It_should_use_create_account_action()
            {
                PerformAction();
                VerifyHandleOfAction<CreateUser, User>();
            }
        }

        public class WhenItIsAGetById : GivenTheUserApiIsCalled
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

        public class WhenItIsAGet : GivenTheUserApiIsCalled
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