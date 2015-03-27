using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
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
    }

    #region Get By Id

    public abstract class GivenThatGetByIdIsSent<TModel, TEntity> : GenericApiControllerTests<TModel, TEntity> where TEntity : class, IEntity, new() where TModel : IApiModel<TEntity>, new()
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

    public abstract class WhenTheEntityDoesntExist<TModel, TEntity> : GivenThatGetByIdIsSent<TModel, TEntity> where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity, new()
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

    public abstract class WhenTheEntityExists<TModel, TEntity> : GivenThatGetByIdIsSent<TModel, TEntity> where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity, new()
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

    #endregion

    #region Get All

    public abstract class GivenThatGetAllIsSent<TModel, TEntity> : GenericApiControllerTests<TModel, TEntity>
        where TEntity : class, IEntity, new()
        where TModel : IApiModel<TEntity>, new()
    {
        [SetUp]
        public virtual void Setup()
        {
            DependencySetup();
        }

        protected HttpResponseMessage PerformAction()
        {
            return GetController().Get();
        }

        [Test]
        public void Then_it_should_call_the_repository_with_the_id()
        {
            PerformAction();

            RepositoryBuilder.Mock.Verify(x => x.GetAll(), Times.Once);
        }
    }

    public abstract class WhenNoEntitiesExist<TModel, TEntity> : GivenThatGetAllIsSent<TModel, TEntity>
        where TModel : IApiModel<TEntity>, new()
        where TEntity : class, IEntity, new()
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

    public abstract class WhenSomeEntitiesExists<TModel, TEntity> : GivenThatGetAllIsSent<TModel, TEntity>
        where TModel : IApiModel<TEntity>, new()
        where TEntity : class, IEntity, new()
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

    #endregion

    #region Search

    public abstract class GivenThatSearchIsSent<TModel, TEntity> : GenericApiControllerTests<TModel, TEntity>
        where TEntity : class, IEntity, new()
        where TModel : IApiModel<TEntity>, new()
    {
        protected string Query;
        [SetUp]
        public virtual void Setup()
        {
            DependencySetup();
            EntitySearchBuilder.WithSuccessfulSearch();
        }

        protected HttpResponseMessage PerformAction()
        {
            return GetController().Get(Query);
        }

        [Test]
        public void Then_it_should_call_the_repository_with_the_id()
        {
            PerformAction();

            EntitySearchBuilder.Mock.Verify(x => x.Search(Query), Times.Once);
        }
    }

    public abstract class WhenNoEntitiesMatchSearch<TModel, TEntity> : GivenThatSearchIsSent<TModel, TEntity>
        where TModel : IApiModel<TEntity>, new()
        where TEntity : class, IEntity, new()
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            EntitySearchBuilder.WithUnsuccessfulSearch();
        }

        [Test]
        public void Then_it_should_return_status_code_not_found()
        {
            var response = PerformAction();

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    public abstract class WhenEntitiesMatchSearch<TModel, TEntity> : GivenThatSearchIsSent<TModel, TEntity>
        where TModel : IApiModel<TEntity>, new()
        where TEntity : class, IEntity, new()
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

    #endregion

    #region PerformAction

    public abstract class GivenAPostIsMade<TModel, TEntity, TAction> : GenericApiControllerTests<TModel, TEntity> where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity, new() where TAction : IAction<TEntity>
    {
        protected TModel Model;

        protected abstract HttpResponseMessage PerformAction();
        [SetUp]
        public virtual void Setup()
        {
            DependencySetup();
            UrlConstructorBuilder.WithUrlConstruction();
            ActionHandlerOverlordBuilder.WithNoErrorsOnHandling<TAction, TEntity>();
            Model = new TModel();
        }
    }

    public abstract class WhenRequestIsValid<TModel, TEntity, TAction> : GivenAPostIsMade<TModel, TEntity, TAction> where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity, new() where TAction : IAction<TEntity>
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ActionHandlerOverlordBuilder.WithNoErrorsOnHandling<EnrolInBlock, User>();
        }

        [Test]
        public void Then_response_should_be_created()
        {
            var response = PerformAction();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }

    public abstract class WhenRequestIsInvalid<TModel, TEntity, TAction> : GivenAPostIsMade<TModel, TEntity, TAction>
        where TModel : IApiModel<TEntity>, new()
        where TEntity : class, IEntity, new()
        where TAction : IAction<TEntity>
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ActionHandlerOverlordBuilder.WithErrorsOnHandling<TAction, TEntity>();
        }

        [Test]
        public void Then_response_should_be_bad_request()
        {
            var response = PerformAction();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

    #endregion

    public class MockEntitySearch<T> : MockBuilder<IEntitySearch<T>> where T : class, new()
    {
        public MockEntitySearch<T> WithSuccessfulSearch()
        {
            Mock.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new List<T>
                {
                    new T()
                });
            return this;
        }

        public MockEntitySearch<T> WithUnsuccessfulSearch()
        {
            Mock.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new List<T>());
            return this;
        }
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
