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
        
        [SetUp]
        public void Setup()
        {
            _userRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithSuccessfulGet()
                .WithUpdate();
            _blockRepositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithSuccessfulGet();

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
        }

        private EnrolInBlockHandler GetHandler()
        {
            return new EnrolInBlockHandler(_userRepositoryBuilder.BuildObject(), _blockRepositoryBuilder.BuildObject());
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
    }
}
