using System.Collections.Generic;
using System.Linq;
using Action;
using ActionHandlers;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class Given_enrol_in_block_is_handled
    {
        private EnrolInBlock _action;

        private MockRepositoryBuilder<User> _userRepositoryBuilder;
        private MockRepositoryBuilder<Block> _blockRepositoryBuilder;
        private MockRepositoryBuilder<Booking> _bookingRepositoryBuilder;
        
        [SetUp]
        public void Setup()
        {
            _userRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithSuccessfulGet()
                .WithUpdate();
            _blockRepositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithGet(new Block
                {
                    Classes = new List<IClass>()
                });

            _action = new EnrolInBlock(new User
            {
                Id = 5,
                EnroledBlocks = new List<IBlock>
                {
                    new Block
                    {
                        Id = 10
                    }
                }
            });
            _bookingRepositoryBuilder = new MockRepositoryBuilder<Booking>()
                .WithGetAll();
        }

        private EnrolInBlockHandler GetHandler()
        {
            return new EnrolInBlockHandler(_userRepositoryBuilder.BuildObject(), _blockRepositoryBuilder.BuildObject(), _bookingRepositoryBuilder.BuildObject());
        }

        private void PerformAction()
        {
            GetHandler().Handle(_action);
        }

        [Test]
        public void Then_the_user_should_be_retrieved()
        {
            PerformAction();

            _userRepositoryBuilder.Mock.Verify(x => x.Get(_action.ActionAgainst.Id));
        }

        [Test]
        public void Then_the_block_being_enroled_in_should_be_retrieved()
        {
            PerformAction();

            _blockRepositoryBuilder.Mock.Verify(x => x.Get(_action.ActionAgainst.EnroledBlocks.Single().Id));
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(8)]
        public void Then_the_users_schedule_should_have_classes_added_to_it(int numberOfClasses)
        {
            var classes = new List<IClass>();
            for (int i = 0; i < numberOfClasses; i++)
            {
                classes.Add(new Class{Id = i});
            }
            _blockRepositoryBuilder.WithGet(new Block {Classes = classes});
            _bookingRepositoryBuilder.WithGetAll(classes.Select(x => new Booking{
                Event = new Class
                {
                    Id = x.Id
                }
            }));

            PerformAction();

            var updatedUser = _userRepositoryBuilder.UpdatedEntity;
            Assert.AreEqual(numberOfClasses, updatedUser.Schedule.Count);
        }
    }
}
