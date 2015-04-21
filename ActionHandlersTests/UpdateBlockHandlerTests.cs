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
    public class UpdateBlockHandlerTests
    {
        private MockRepositoryBuilder<Block> _repositoryBuilder;
        private UpdateBlock _action;
        private Block _existingBlock;

        private void PerformAction()
        {
            new UpdateBlockHandler(_repositoryBuilder.BuildObject()).Handle(_action);
        }

        [SetUp]
        public void Setup()
        {
            _existingBlock = new Block
            {
                Name = "old",
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue
            };
            _action = new UpdateBlock(new Block{Name = "new", StartDate = DateTime.MinValue, EndDate = DateTime.MinValue});
            _repositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithGet(_existingBlock)
                .WithUpdate();
        }

        [Test]
        public void It_should_update_name()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Block>()));
            Assert.AreEqual("new", _repositoryBuilder.UpdatedEntity.Name);
        }

        [Test]
        public void It_should_not_update_any_other_fields()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Block>()));
            Assert.AreNotEqual(_action.ActionAgainst.EndDate, _repositoryBuilder.UpdatedEntity.EndDate);
            Assert.AreNotEqual(_action.ActionAgainst.StartDate, _repositoryBuilder.UpdatedEntity.StartDate);
        }
    }
}
