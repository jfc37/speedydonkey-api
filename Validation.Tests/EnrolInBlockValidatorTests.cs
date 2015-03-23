using System.Collections.Generic;
using System.Linq;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class EnrolInBlockValidatorTests
    {
        private User _user;
        private MockRepositoryBuilder<User> _userRepositoryBuilder;
        private MockRepositoryBuilder<Block> _blockRepositoryBuilder;
        
        [SetUp]
        public void Setup()
        {
            _userRepositoryBuilder = new MockRepositoryBuilder<User>().WithGet(new User
            {
                EnroledBlocks = new List<IBlock>()
            });
            _blockRepositoryBuilder = new MockRepositoryBuilder<Block>().WithGetAll(new List<Block>
            {
                new Block {Id = 2}
            });
            _user = new User
            {
                EnroledBlocks = new List<IBlock>
                {
                    new Block{ Id = 2}
                }
            };
        }

        private EnrolInBlockValidator GetValidator()
        {
            return new EnrolInBlockValidator(
                _userRepositoryBuilder.BuildObject(),
                _blockRepositoryBuilder.BuildObject());
        }

        private FluentValidation.Results.ValidationResult PerforAction()
        {
            return GetValidator().Validate(_user);
        }

        [Test]
        public void When_it_is_valid_then_no_validation_errors_should_be_returned()
        {
            var result = PerforAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void When_user_is_already_enroled_in_a_selected_block_it_should_return_error()
        {
            _userRepositoryBuilder.WithGet(new User
            {
                EnroledBlocks = new List<IBlock>
                {
                    new Block {Id = 2}
                }
            });
            _user.EnroledBlocks = new List<IBlock>{ new Block{ Id = 2 }};

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var message = result.Errors.Single().ErrorMessage;
            Assert.AreEqual(ValidationMessages.AlreadyEnroledInBlock, message);
        }

        [Test]
        public void When_selected_block_ids_dont_exist_then_it_should_return_error()
        {
            _blockRepositoryBuilder.WithGetAll(new List<Block>
            {
                new Block {Id = 1}
            });
            _user.EnroledBlocks = new List<IBlock>{ new Block{ Id = 4 }};

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var message = result.Errors.Single().ErrorMessage;
            Assert.AreEqual(ValidationMessages.InvalidBlock, message);
        }
    }
}
