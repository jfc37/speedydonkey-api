using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using Moq;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class EnrolInBlockValidatorTests
    {
        private User _user;
        private Mock<IRepository<User>> _userRepository;
        private Mock<IRepository<Block>> _blockRepository;
        private CurrentUser _currentUser;
        
        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IRepository<User>>(MockBehavior.Loose);
            _userRepository.SetReturnsDefault(new User
            {
                EnroledBlocks = new List<Block>(),
                Claims = Claim.Teacher.ToString()
            });

            _blockRepository = new Mock<IRepository<Block>>(MockBehavior.Loose);
            _blockRepository.SetReturnsDefault(new List<Block>
            {
                new Block {Id = 2}
            }.AsEnumerable());
            _user = new User
            {
                Id = 1,
                EnroledBlocks = new List<Block>
                {
                    new Block{ Id = 2}
                }
            };
            _currentUser = new CurrentUser
            {
                Id = 1
            };
        }

        private EnrolInBlockValidator GetValidator()
        {
            return new EnrolInBlockValidator(
                _userRepository.Object,
                _blockRepository.Object,
                _currentUser);
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
            _userRepository.SetReturnsDefault(new User
            {
                EnroledBlocks = new List<Block>
                {
                    new Block {Id = 2}
                }
            });
            _user.EnroledBlocks = new List<Block>{ new Block{ Id = 2 }};

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var message = result.Errors.Single().ErrorMessage;
            Assert.AreEqual(ValidationMessages.AlreadyEnroledInBlock, message);
        }

        [Test]
        public void When_selected_block_ids_dont_exist_then_it_should_return_error()
        {
            _blockRepository.SetReturnsDefault(new List<Block>
            {
                new Block {Id = 1}
            });
            _user.EnroledBlocks = new List<Block>{ new Block{ Id = 4 }};

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var message = result.Errors.Single().ErrorMessage;
            Assert.AreEqual(ValidationMessages.InvalidBlock, message);
        }

        [Test]
        public void When_student_being_enroled_isnt_current_user_and_doesnt_have_claim_then_it_should_return_error()
        {
            _user.Id = 1;
            _currentUser = new CurrentUser { Id = 2 };
            _userRepository.SetReturnsDefault(new User
            {
                EnroledBlocks = new List<Block>(),
            });

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var message = result.Errors.Single().ErrorMessage;
            Assert.AreEqual(ValidationMessages.InvalidUserToEnrol, message);
        }

        [Test]
        public void When_block_is_invite_only_and_the_user_isnt_a_teacher_then_it_should_return_error()
        {
            _blockRepository.SetReturnsDefault(new Block(2){IsInviteOnly = true}.PutIntoList().AsEnumerable());
            _userRepository.SetReturnsDefault(new User());

            var result = PerforAction();

            Assert.IsFalse(result.IsValid);
            var message = result.Errors.Single().ErrorMessage;
            Assert.AreEqual(ValidationMessages.UnavailableBlock, message);
        }

        [Test]
        public void When_block_is_not_invite_only_and_the_user_isnt_a_teacher_then_no_error_returned()
        {
            _blockRepository.SetReturnsDefault(new Block(2){IsInviteOnly = false}.PutIntoList().AsEnumerable());
            _userRepository.SetReturnsDefault(new User());

            var result = PerforAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void When_block_is_invite_only_and_the_user_is_a_teacher_then_no_error_returned()
        {
            _blockRepository.SetReturnsDefault(new Block(2){IsInviteOnly = true}.PutIntoList().AsEnumerable());
            _userRepository.SetReturnsDefault(new User{Claims = Claim.Teacher.ToString()});

            var result = PerforAction();

            Assert.IsTrue(result.IsValid);
        }
    }
}
