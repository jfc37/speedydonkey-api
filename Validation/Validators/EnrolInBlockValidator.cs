using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class EnrolInBlockValidator : AbstractValidator<User>, IActionValidator<EnrolInBlock, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Block> _blockRepository;
        private readonly ICurrentUser _currentUser;

        public EnrolInBlockValidator(IRepository<User> userRepository, IRepository<Block> blockRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _blockRepository = blockRepository;
            _currentUser = currentUser;
            When(x => x.EnroledBlocks != null, () =>
            {
                RuleFor(x => x.EnroledBlocks).Must(NotAlreadyBeEnroled)
                    .WithMessage(ValidationMessages.AlreadyEnroledInBlock)
                    .Must(BeExistingBlocks).WithMessage(ValidationMessages.InvalidBlock);
                RuleFor(x => x.Id).Must(BeAllowedToEnrol).WithMessage(ValidationMessages.InvalidUserToEnrol);
            });
        }

        private bool BeAllowedToEnrol(int userIdBeingEnroled)
        {
            if (userIdBeingEnroled == _currentUser.Id)
                return true;

            var user = _userRepository.Get(_currentUser.Id);
            return user.Claims != null && user.Claims.Contains(Claim.Teacher.ToString());
        }

        private bool BeExistingBlocks(ICollection<IBlock> blocks)
        {
            return _blockRepository.GetAll().Select(x => x.Id).Intersect(blocks.Select(x => x.Id)).Count() == blocks.Count;
        }

        private bool NotAlreadyBeEnroled(User user, ICollection<IBlock> blocksBeingEnroledIn)
        {
            var alreadyEnroledBlockIds = _userRepository.GetWithChildren(user.Id, new List<string>{"EnroledBlocks"}).EnroledBlocks.Select(x => x.Id);
            var enrolingBlockIds = blocksBeingEnroledIn.Select(x => x.Id);
            return !alreadyEnroledBlockIds.Intersect(enrolingBlockIds).Any();
        }
    }
}
