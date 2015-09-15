using System;
using System.Collections.Generic;
using Action;
using ActionHandlers.CreateHandlers;
using ActionHandlers.CreateHandlers.Strategies;
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
        protected MockRepositoryBuilder<Class> ClassRepositoryBuilder;
        protected MockRepositoryBuilder<Booking> BookingRepositoryBuilder; 
        protected CreateBlock Action;
        protected DateTime LevelStartTime;
        protected DateTime LevelEndTime;

        [SetUp]
        public virtual void Setup()
        {
            LevelStartTime = DateTime.Now;
            LevelEndTime = LevelStartTime.AddHours(1);
            RepositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithCreate();
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                .WithGet(GetStandardLevel());
            ClassRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithCreate();
            BookingRepositoryBuilder = new MockRepositoryBuilder<Booking>()
                .WithCreate();
            Action = new CreateBlock(new Block
            {
                Level = new Level()
            });
        }

        protected Level GetStandardLevel()
        {
            return new Level
            {
                Blocks = new List<Block>(),
                Teachers = new List<Teacher> { new Teacher() },
                ClassesInBlock = 1
            };
        }

        private CreateBlockHandler GetHandler()
        {
            return new CreateBlockHandler(
                RepositoryBuilder.BuildObject(), 
                LevelRepositoryBuilder.BuildObject(),
                ClassRepositoryBuilder.BuildObject(),
                BookingRepositoryBuilder.BuildObject(),
                new BlockPopulatorStrategyFactory());
        }

        protected void PerformAction()
        {
            GetHandler().Handle(Action);
        }
    }

    public class WhenLevelDoesntHaveAnyBlocks : GivenCreateBlockIsHandled
    {
        public override void Setup()
        {
            base.Setup();
            var level = GetStandardLevel();
            level.StartTime = DateTime.Now;
            level.EndTime = DateTime.Now.AddHours(1);
            level.Name = "My Name";
            RepositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithCreate();
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                .WithGet(level);
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

        [Test]
        public void Then_block_should_get_the_level_name()
        {
            PerformAction();

            var createdBlock = RepositoryBuilder.CreatedEntity;
            var level = Action.ActionAgainst.Level;
            Assert.AreEqual(level.Name, createdBlock.Name);
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(8)]
        public void Then_the_number_of_classes_in_the_block_should_match_the_number_of_weeks_the_block_runs(int numberOfClasses)
        {
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                   .WithGet(new Level
                   {
                       StartTime = LevelStartTime,
                       EndTime = LevelEndTime,
                       ClassesInBlock = numberOfClasses,
                       Blocks = new List<Block>(),
                       Teachers = new[]
                        {
                            new Teacher(), 
                        }
                   });

            PerformAction();

            var createdBlock = RepositoryBuilder.CreatedEntity;
            var expectedEndDate = LevelStartTime.AddDays((numberOfClasses - 1)*7);
            Assert.AreEqual(expectedEndDate, createdBlock.EndDate);
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(8)]
        public void Then_the_correct_number_of_classes_should_be_created(int numberOfClasses)
        {
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                   .WithGet(new Level
                   {
                       StartTime = LevelStartTime,
                       EndTime = LevelEndTime,
                       ClassesInBlock = numberOfClasses,
                       Blocks = new List<Block>(),
                       Teachers = new[]
                        {
                            new Teacher(), 
                        }
                   });

            PerformAction();

            ClassRepositoryBuilder.Mock.Verify(x => x.Create(It.IsAny<Class>()), Times.Exactly(numberOfClasses));
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(8)]
        public void Then_bookings_should_be_created_for_each_class(int numberOfClasses)
        {
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                   .WithGet(new Level
                   {
                       StartTime = LevelStartTime,
                       EndTime = LevelEndTime,
                       ClassesInBlock = numberOfClasses,
                       Blocks = new List<Block>(),
                       Teachers = new[]
                        {
                            new Teacher(), 
                        }
                   });

            PerformAction();

            BookingRepositoryBuilder.Mock.Verify(x => x.Create(It.IsAny<Booking>()), Times.Exactly(numberOfClasses));
        }
    }

    public class WhenLevelHasABlockCurrentlyInProgress : GivenCreateBlockIsHandled
    {
        public override void Setup()
        {
            base.Setup();
            var level = GetStandardLevel();
            level.Blocks = new[]
            {
                new Block
                {
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now.AddDays(4)
                }
            };
            RepositoryBuilder.WithCreate();
            LevelRepositoryBuilder.WithGet(level);
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(8)]
        public void Then_the_correct_number_of_classes_should_be_created(int numberOfClasses)
        {
            LevelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                   .WithGet(new Level
                   {
                       StartTime = LevelStartTime,
                       EndTime = LevelEndTime,
                       ClassesInBlock = numberOfClasses,
                       Blocks = new List<Block>(),
                       Teachers = new[]
                        {
                            new Teacher(), 
                        }
                   });

            PerformAction();

            ClassRepositoryBuilder.Mock.Verify(x => x.Create(It.IsAny<Class>()), Times.Exactly(numberOfClasses));
        }
    }
}
