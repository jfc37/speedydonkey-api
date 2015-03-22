using System.Net.Http;
using Action;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Post

    public class EnrolmentApiWhenRequestIsValid : WhenRequestIsValid<UserModel, User, EnrolInBlock>
    {
        private int _levelId;
        private EnrolmentModel _model;

        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new EnrolmentApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((EnrolmentApiController)GetController()).Post(_levelId, _model);
        }

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _model = new EnrolmentModel();
        }
    }

    public class EnrolmentApiWhenRequestIsInvalid : WhenRequestIsInvalid<UserModel, User, EnrolInBlock>
    {
        private int _levelId;
        private EnrolmentModel _model;
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new EnrolmentApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _model = new EnrolmentModel();
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((EnrolmentApiController)GetController()).Post(_levelId, _model);
        }
    }

    #endregion
}