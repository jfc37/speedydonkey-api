using System;
using Action;
using ActionHandlers.UpdateHandlers;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateClassHandlerTests
    {
        private MockRepositoryBuilder<Class> _repositoryBuilder;
        private UpdateClass _action;
        private Class _existingClass;

        private void PerformAction()
        {
            new UpdateClassHandler(_repositoryBuilder.BuildObject()).Handle(_action);
        }

        [SetUp]
        public void Setup()
        {
            _existingClass = new Class
            {
                Name = "old",
                StartTime = DateTime.MaxValue,
                EndTime = DateTime.MaxValue
            };
            _action = new UpdateClass(new Class { Name = "new", StartTime = DateTime.MinValue, EndTime = DateTime.MinValue });
            _repositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGet(_existingClass)
                .WithUpdate();
        }

        [Test]
        public void It_should_update_name()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Class>()));
            Assert.AreEqual("new", _repositoryBuilder.UpdatedEntity.Name);
        }

        [Test]
        public void It_should_update_start_time()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Class>()));
            Assert.AreEqual(DateTime.MinValue, _repositoryBuilder.UpdatedEntity.StartTime);
        }

        [Test]
        public void It_should_update_end_time()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Class>()));
            Assert.AreEqual(DateTime.MinValue, _repositoryBuilder.UpdatedEntity.EndTime);
        }
    }
}
