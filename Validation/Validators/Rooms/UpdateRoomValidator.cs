using Action.Rooms;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Rooms
{
    public class UpdateRoomValidator : AbstractValidator<Room>, IActionValidator<UpdateRoom, Room>
    {
        public UpdateRoomValidator(IRepository<Room> repository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Room>(repository, x).IsValid()).WithMessage(ValidationMessages.InvalidRoom);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage(ValidationMessages.MissingLocation);
        }
    }
}