using Action.Blocks;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Blocks
{
    public class UnassignBlockRoomValidator : AbstractValidator<Block>, IActionValidator<UnassignBlockRoom, Block>
    {
        public UnassignBlockRoomValidator(IRepository<Block> blockRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Block>(blockRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidBlock);
        }
    }
}