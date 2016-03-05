using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Common.Extensions;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

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
            When(x => x.EnroledBlocks.IsNotNull(), () =>
            {
                RuleFor(x => x.EnroledBlocks)
                    .Must(NotAlreadyBeEnroled).WithMessage(ValidationMessages.AlreadyEnroledInBlock)
                    .Must(BeExistingBlocks).WithMessage(ValidationMessages.InvalidBlock)
                    .Must(ComplyWithInviteOnlyRule).WithMessage(ValidationMessages.UnavailableBlock);
                RuleFor(x => x.Id)
                    .Must(BeAllowedToEnrol).WithMessage(ValidationMessages.InvalidUserToEnrol);
            });
        }

        private bool ComplyWithInviteOnlyRule(ICollection<Block> blocks)
        {
            var isUserATeacher = IsUserATeacher();

            var blockIds = blocks.Select(x => x.Id);
            var isEnrollingInInviteOnly = _blockRepository.Queryable()
                .Where(x => blockIds.Contains(x.Id))
                .Any(x => x.IsInviteOnly);

            return !isEnrollingInInviteOnly || isUserATeacher;
        }

        private bool BeAllowedToEnrol(int userIdBeingEnroled)
        {
            if (userIdBeingEnroled == _currentUser.Id)
                return true;

            return IsUserATeacher();
        }

        private bool IsUserATeacher()
        {
            var user = _userRepository.Get(_currentUser.Id);
            return new DoesUserHaveClaimRule(user, Claim.Teacher)
                .IsValid();
        }

        private bool BeExistingBlocks(ICollection<Block> blocks)
        {
            return _blockRepository.Queryable()
                .Select(x => x.Id)
                .Intersect(blocks.Select(x => x.Id)).Count() == blocks.Count;
        }

        private bool NotAlreadyBeEnroled(User user, ICollection<Block> blocksBeingEnroledIn)
        {
            var alreadyEnroledBlockIds = _userRepository.GetWithChildren(user.Id, new List<string>{"EnroledBlocks"}).EnroledBlocks.Select(x => x.Id);
            var enrolingBlockIds = blocksBeingEnroledIn.Select(x => x.Id);
            return !alreadyEnroledBlockIds.Intersect(enrolingBlockIds).Any();
        }
    }
}
