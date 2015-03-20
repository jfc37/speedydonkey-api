using System.Net.Http;
using System.Web.Http;
using ActionHandlersTests.Builders.MockBuilders;
using Actions;
using Common.Tests.Builders.MockBuilders;
using Data.Searches;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace StudyBuddyApi.Tests.Controllers
{
    [TestFixture]
    public abstract class GenericApiControllerTests<TEntity> where TEntity : class, IEntity, new()
    {
        protected MockActionHandlerOverlordBuilder ActionHandlerOverlordBuilder;
        protected MockUrlConstructorBuilder UrlConstructorBuilder;
        protected MockRepositoryBuilder<TEntity> RepositoryBuilder;
        protected MockEntitySearch<TEntity> EntitySearchBuilder;

        protected void DependencySetup()
        {
            ActionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            UrlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
            RepositoryBuilder = new MockRepositoryBuilder<TEntity>()
                .WithSuccessfulGet();
            EntitySearchBuilder = new MockEntitySearch<TEntity>();
        }

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
