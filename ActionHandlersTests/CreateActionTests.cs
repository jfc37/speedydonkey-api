using ActionHandlers.CreateHandlers;
using Actions;
using Common.Tests.Builders.MockBuilders;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public abstract class CreateActionTests<TEntity> where TEntity : class, IEntity, new()
    {
        protected MockRepositoryBuilder<TEntity> RepositoryBuilder;

        protected void SetupDependencies()
        {
            RepositoryBuilder = new MockRepositoryBuilder<TEntity>()
                .WithCreate();
        }

        protected void CheckCreateWasCalled()
        {
            RepositoryBuilder.Mock.Verify(x => x.Create(It.IsAny<TEntity>()), Times.Once);
        }
    }

    public class GivenCreateUserIsHandled : CreateActionTests<User>
    {
        private MockPasswordHasherBuilder _passwordHasherBuilder;
        private CreateUser _action;

        [SetUp]
        public void Setup()
        {
            SetupDependencies();
            _passwordHasherBuilder = new MockPasswordHasherBuilder()
                .WithHashCreation();

            _action = new CreateUser(new User());
        }

        private CreateUserHandler GetHandler()
        {
            return new CreateUserHandler(RepositoryBuilder.BuildObject(), _passwordHasherBuilder.BuildObject());
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
    }
}
