using System;
using System.Collections.Generic;
using Action;
using ActionHandlers.CreateHandlers;
using ActionHandlers.CreateHandlers.Strategies;
using Data.Repositories;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class GivenCreateBlockIsHandled
    {
        protected MockRepositoryBuilder<Block> RepositoryBuilder;
        protected MockRepositoryBuilder<Level> LevelRepositoryBuilder;
        protected CreateBlock Action;

        protected void OverallSetup()
        {
            RepositoryBuilder = new MockRepositoryBuilder<Block>();
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>();
            Action = new CreateBlock(new Block
            {
                Level = new Level()
            });
        }

        protected CreateBlockHandler GetHandler()
        {
            return new CreateBlockHandler(
                RepositoryBuilder.BuildObject(), 
                LevelRepositoryBuilder.BuildObject(),
                new BlockPopulatorStrategyFactory());
        }

        protected void PerformAction()
        {
            GetHandler().Handle(Action);
        }
    }

    public class WhenLevelDoesntHaveAnyBlocks : GivenCreateBlockIsHandled
    {
        [SetUp]
        public void Setup()
        {
            OverallSetup();
            RepositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithCreate();
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                .WithGet(new Level
                {
                    StartTime = DateTime.Now,
                    Blocks = new List<IBlock>()
                });
        }

        [Test]
        public void Then_block_should_be_created()
        {
            PerformAction();

            RepositoryBuilder.Mock.Verify(x => x.Create(Action.ActionAgainst));
        }

        [Test]
        public void Then_block_should_start_when_level_starts()
        {
            PerformAction();

            var createdBlock = RepositoryBuilder.CreatedEntity;
            var level = Action.ActionAgainst.Level;
            Assert.AreEqual(level.StartTime, createdBlock.StartDate);
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(8)]
        public void Then_the_number_of_classes_in_the_block_should_match_the_number_of_weeks_the_block_runs(int numberOfClasses)
        {
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                   .WithGet(new Level
                   {
                       StartTime = DateTime.Now,
                       ClassesInBlock = numberOfClasses,
                       Blocks = new List<IBlock>()
                   });

            PerformAction();

            var createdBlock = RepositoryBuilder.CreatedEntity;
            var expectedEndDate = createdBlock.StartDate.AddDays(numberOfClasses*7);
            Assert.AreEqual(expectedEndDate, createdBlock.EndDate);
        }
    }

    public class WhenLevelAlreadyHasABlockStartingInTheFuture : GivenCreateBlockIsHandled
    {
        [SetUp]
        public void Setup()
        {
            OverallSetup();
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>().WithGet(new Level
            {
                Blocks = new[]
                {
                    new Block
                    {
                        StartDate = DateTime.Now.AddDays(1)
                    }
                }
            });
        }

        [Test]
        public void Then_no_new_block_should_be_created()
        {
            PerformAction();

            RepositoryBuilder.Mock.Verify(x => x.Create(It.IsAny<Block>()), Times.Never);
        }
    }

    public class WhenLevelHasABlockCurrentlyInProgress : GivenCreateBlockIsHandled
    {
        [SetUp]
        public void Setup()
        {
            OverallSetup();
            RepositoryBuilder.WithCreate();
            LevelRepositoryBuilder.WithGet(new Level
            {
                Blocks = new[]
                {
                    new Block
                    {
                        StartDate = DateTime.Now.AddMonths(-1),
                        EndDate = DateTime.Now.AddDays(4)
                    }
                }
            });
        }

        [Test]
        public void Then_block_should_be_created()
        {
            PerformAction();

            RepositoryBuilder.Mock.Verify(x => x.Create(Action.ActionAgainst), Times.Once);
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(12)]
        public void Then_block_should_start_the_week_after_current_block_finishes(int daysUntilFinish)
        {
            var currentBlockEndDate = DateTime.Now.AddDays(daysUntilFinish);
            LevelRepositoryBuilder.WithGet(new Level
            {
                Blocks = new[]
                {
                    new Block
                    {
                        StartDate = DateTime.Now.AddMonths(-1),
                        EndDate = currentBlockEndDate
                    }
                }
            });

            PerformAction();

            var createdBlock = RepositoryBuilder.CreatedEntity;
            var expectedStartDate = currentBlockEndDate.AddDays(7);
            Assert.AreEqual(expectedStartDate, createdBlock.StartDate);
        }
    }
}
