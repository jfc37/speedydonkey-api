using System.Collections.Generic;
using Action;
using ActionHandlers;
using ActionHandlersTests.Builders.MockBuilders;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class GivenGenerateBlocksForAllLevelsIsRequested
    {
        private GenerateBlocksForAllLevels _action;

        private MockRepositoryBuilder<Level> _repositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;

        [SetUp]
        public virtual void Setup()
        {
            _action = new GenerateBlocksForAllLevels(new Block());

            _repositoryBuilder = new MockRepositoryBuilder<Level>()
                .WithSuccessfulGet();

            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder()
                .WithNoErrorsOnHandling<CreateBlock, Block>();
        }

        private void PerformAction()
        {
            new GenerateBlocksForAllLevelsHandler(_repositoryBuilder.BuildObject(), _actionHandlerOverlordBuilder.BuildObject())
                .Handle(_action);
        }

        public class WhenThereAreNoLevels : GivenGenerateBlocksForAllLevelsIsRequested
        {
            public override void Setup()
            {
                base.Setup();
                _repositoryBuilder.WithUnsuccessfulGet();
            }

            [Test]
            public void Then_no_blocks_are_generated()
            {
                PerformAction();

                _actionHandlerOverlordBuilder.Mock.Verify(x => x.HandleAction<CreateBlock, Block>(It.IsAny<CreateBlock>()), Times.Never);
            }
        }

        public class WhenThereIsOneLevel : GivenGenerateBlocksForAllLevelsIsRequested
        {
            public override void Setup()
            {
                base.Setup();
                _repositoryBuilder.WithSuccessfulGet();
            }

            [Test]
            public void Then_one_block_is_generated()
            {
                PerformAction();

                _actionHandlerOverlordBuilder.Mock.Verify(x => x.HandleAction<CreateBlock, Block>(It.IsAny<CreateBlock>()), Times.Once);
            }
        }

        public class WhenThereAreMultipleLevels : GivenGenerateBlocksForAllLevelsIsRequested
        {
            public override void Setup()
            {
                base.Setup();
                _repositoryBuilder.WithGetAll();
            }

            [TestCase(2)]
            [TestCase(4)]
            [TestCase(9)]
            public void Then_multiples_blocks_are_generated(int numberOfLevels)
            {
                var allLevels = new List<Level>();
                for (int i = 0; i < numberOfLevels; i++)
                {
                    allLevels.Add(new Level());
                }
                _repositoryBuilder.WithGetAll(allLevels);

                PerformAction();

                _actionHandlerOverlordBuilder.Mock.Verify(x => x.HandleAction<CreateBlock, Block>(It.IsAny<CreateBlock>()), Times.Exactly(numberOfLevels));
            }
        }

    }
}
