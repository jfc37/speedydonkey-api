using System.Collections.Generic;
using System.Linq;
using Action;
using ActionHandlers.EnrolmentProcess;
using Data.Tests.Builders;
using Models;
using Moq;
using Notification.Notifications;
using Notification.Tests.Builders;
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
        private MockPostOfficeBuilder _postOfficeBuilder;
        
        [SetUp]
        public void Setup()
        {
            _userRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithSuccessfulGet()
                .WithUpdate();
            _blockRepositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithGetAll(new [] {
                    new Block
                    {
                        Classes = new List<IClass>()
                    }
                });

            _action = new EnrolInBlock(new User
            {
                Id = 5,
                Passes = new List<IPass>(),
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
            _postOfficeBuilder = new MockPostOfficeBuilder()
                .WithSending();
        }

        private EnrolInBlockHandler GetHandler()
        {
            return new EnrolInBlockHandler(
                _userRepositoryBuilder.BuildObject(),
                _postOfficeBuilder.BuildObject());
        }

        private void PerformAction()
        {
            GetHandler().Handle(_action);
        }

        [Test]
        public void Then_the_user_should_be_retrieved()
        {
            PerformAction();

            _userRepositoryBuilder.Mock.Verify(x => x.GetWithChildren(_action.ActionAgainst.Id, It.IsAny<IList<string>>()));
        }

        [Test]
        public void Then_the_block_being_enroled_in_should_be_retrieved()
        {
            PerformAction();

            _blockRepositoryBuilder.Mock.Verify(x => x.GetAllWithChildren(It.IsAny<IList<string>>()));
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
            _blockRepositoryBuilder.WithGetAll(new [] {new Block
            {
                Id = _action.ActionAgainst.EnroledBlocks.Single().Id,
                Classes = classes
            }});
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

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(8)]
        public void Then_the_class_rolls_should_include_user(int numberOfClasses)
        {
            var classes = new List<IClass>();
            for (int i = 0; i < numberOfClasses; i++)
            {
                classes.Add(new Class{Id = i});
            }
            _blockRepositoryBuilder.WithGetAll(new [] {new Block
            {
                Id = _action.ActionAgainst.EnroledBlocks.Single().Id,
                Classes = classes
            }});
            _bookingRepositoryBuilder.WithGetAll(classes.Select(x => new Booking{
                Event = new Class
                {
                    Id = x.Id
                }
            }));

            PerformAction();

            var updatedUser = _userRepositoryBuilder.UpdatedEntity;
            foreach (var blockClass in _blockRepositoryBuilder.BuildObject().GetAll().SelectMany(x => x.Classes))
            {
                Assert.Contains(updatedUser, blockClass.RegisteredStudents.ToArray());
            }
        }

        [Test]
        public void Then_it_should_send_out_an_email()
        {
            PerformAction();

            _postOfficeBuilder.Mock.Verify(x => x.Send(It.IsAny<UserEnroledInBlock>()));
        }
    }
}
