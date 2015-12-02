using Action.Blocks;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Blocks
{
    public class ChangeBlockRoomValidator : AbstractValidator<Block>, IActionValidator<ChangeBlockRoom, Block>
    {
        public ChangeBlockRoomValidator(IRepository<Block> blockRepository, IRepository<Room> roomRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Block>(blockRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidBlock);

            RuleFor(x => x.Room)
                .NotEmpty().WithMessage(ValidationMessages.RoomRequired)
                .Must(x => new DoesIdExist<Room>(roomRepository, x.Id).IsValid()).WithMessage(ValidationMessages.InvalidRoom);
        }
    }
}