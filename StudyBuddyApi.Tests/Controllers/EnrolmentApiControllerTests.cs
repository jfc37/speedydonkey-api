using System.Linq;
using System.Net;
using System.Net.Http;
using Action;
using ActionHandlersTests.Builders.MockBuilders;
using Common;
using Common.Tests.Builders.MockBuilders;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace StudyBuddyApi.Tests.Controllers
{
    [TestFixture]
    public class EnrolmentApiControllerTests
    {
        protected MockActionHandlerOverlordBuilder ActionHandlerOverlordBuilder;
        protected MockUrlConstructorBuilder UrlConstructorBuilder;
        protected MockRepositoryBuilder<User> RepositoryBuilder;
        protected ICommonInterfaceCloner Cloner;
        protected MockEntitySearch<User> EntitySearchBuilder; 

        protected EnrolmentApiController GetController()
        {
            var controller = new EnrolmentApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
            ApiControllerSetup.Setup(controller);
            return controller;
        }

        [SetUp]
        public virtual void Setup()
        {
            ActionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            UrlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
            RepositoryBuilder = new MockRepositoryBuilder<User>();
            Cloner = new CommonInterfaceCloner();
            EntitySearchBuilder = new MockEntitySearch<User>();
        }
    }

    public class GivenAPostIsMade : EnrolmentApiControllerTests
    {
        protected int Id;
        protected EnrolmentModel Model;

        protected HttpResponseMessage PerformAction()
        {
            return GetController().Post(Id, Model);
        }
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ActionHandlerOverlordBuilder.WithNoErrorsOnHandling<EnrolInBlock, User>();
            Model = new EnrolmentModel();
        }

        [Test]
        public void Then_model_user_id_should_be_set_from_url()
        {
            Id = 43;

            PerformAction();

            Assert.AreEqual(Id, ((EnrolInBlock) ActionHandlerOverlordBuilder.PassedInAction).ActionAgainst.Id);
        }

        [Test]
        public void Then_block_ids_should_be_included_in_action()
        {
            Model = new EnrolmentModel
            {
                BlockIds = new []{1, 2, 3}
            };

            PerformAction();

            Assert.AreEqual(Model.BlockIds, ((EnrolInBlock) ActionHandlerOverlordBuilder.PassedInAction).ActionAgainst.EnroledBlocks.Select(x => x.Id));
        }

        [Test]
        public void Then_pass_types_should_be_included_in_action()
        {
            Model = new EnrolmentModel
            {
                PassTypes = new []{PassType.Clip.ToString()}
            };

            PerformAction();

            Assert.AreEqual(Model.PassTypes, ((EnrolInBlock) ActionHandlerOverlordBuilder.PassedInAction).ActionAgainst.Passes.Select(x => x.PassType.ToString()));
        }
    }

    public class WhenRequestIsValid : GivenAPostIsMade
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ActionHandlerOverlordBuilder.WithNoErrorsOnHandling<EnrolInBlock, User>();
            UrlConstructorBuilder.WithUrlConstruction();
            Model = new EnrolmentModel();
        }

        [Test]
        public void Then_response_should_be_created()
        {
            var response = PerformAction();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }

    public class WhenRequestIsInvalid : GivenAPostIsMade
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ActionHandlerOverlordBuilder.WithErrorsOnHandling<EnrolInBlock, User>();
            UrlConstructorBuilder.WithUrlConstruction();
            Model = new EnrolmentModel();
        }

        [Test]
        public void Then_response_should_be_bad_request()
        {
            var response = PerformAction();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
