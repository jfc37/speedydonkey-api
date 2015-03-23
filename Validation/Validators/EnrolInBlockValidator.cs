using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class EnrolInBlockValidator : AbstractValidator<User>, IActionValidator<EnrolInBlock, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Block> _blockRepository;

        public EnrolInBlockValidator(IRepository<User> userRepository, IRepository<Block> blockRepository)
        {
            _userRepository = userRepository;
            _blockRepository = blockRepository;
            When(x => x.EnroledBlocks != null, () =>
            {
                RuleFor(x => x.EnroledBlocks).Must(NotAlreadyBeEnroled).WithMessage(ValidationMessages.AlreadyEnroledInBlock);
                RuleFor(x => x.EnroledBlocks).Must(BeExistingBlocks).WithMessage(ValidationMessages.InvalidBlock);
            });
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
