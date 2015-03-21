using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlersTests.Builders.MockBuilders;
using Actions;
using Common;
using Common.Tests.Builders.MockBuilders;
using Data.Searches;
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
    public abstract class GenericApiControllerTests<TModel, TEntity>  where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity, new()
    {
        protected MockActionHandlerOverlordBuilder ActionHandlerOverlordBuilder;
        protected MockUrlConstructorBuilder UrlConstructorBuilder;
        protected MockRepositoryBuilder<TEntity> RepositoryBuilder;
        protected MockEntitySearch<TEntity> EntitySearchBuilder;
        protected ICommonInterfaceCloner Cloner;

        protected void DependencySetup()
        {
            ActionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            UrlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
            RepositoryBuilder = new MockRepositoryBuilder<TEntity>()
                .WithSuccessfulGet();
            EntitySearchBuilder = new MockEntitySearch<TEntity>();
            Cloner = new CommonInterfaceCloner();
        }

        protected GenericApiController<TModel, TEntity> GetController()
        {
            var controller = GetContreteController();
            ApiControllerSetup.Setup(controller);
            return controller;
        }
        protected abstract GenericApiController<TModel, TEntity> GetContreteController();

        protected void SetupActionHandler<TAction>() where TAction : IAction<TEntity>
        {
            ActionHandlerOverlordBuilder.WithNoErrorsOnHandling<TAction, TEntity>();
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

    public abstract class GivenThatAGetIsSent<TModel, TEntity> : GenericApiControllerTests<TModel, TEntity> where TEntity : class, IEntity, new() where TModel : IApiModel<TEntity>, new()
    {
        protected int Id;

        [SetUp]
        public virtual void Setup()
        {
            DependencySetup();
            RepositoryBuilder.WithSuccessfulGet();
            Id = 42;
        }

        protected HttpResponseMessage PerformAction()
        {
            return GetController().Get(Id);
        }

        [Test]
        public void Then_it_should_call_the_repository_with_the_id()
        {
            PerformAction();

            RepositoryBuilder.Mock.Verify(x => x.Get(Id), Times.Once);
        }
    }

    public abstract class WhenTheEntityDoesntExist<TModel, TEntity> : GivenThatAGetIsSent<TModel, TEntity> where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity, new()
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            RepositoryBuilder.WithUnsuccessfulGet();
        }

        [Test]
        public void Then_it_should_return_status_code_not_found()
        {
            var response = PerformAction();

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    public abstract class WhenTheEntityExists<TModel, TEntity> : GivenThatAGetIsSent<TModel, TEntity> where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity, new()
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            RepositoryBuilder.WithSuccessfulGet();
        }

        [Test]
        public void Then_it_should_return_status_code_ok()
        {
            var response = PerformAction();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }


    public class MockEntitySearch<T> : MockBuilder<IEntitySearch<T>> where T : class
    {
    }

    public static class ApiControllerSetup
    {
        public static void Setup(ApiController controller)
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }
    }
}
