using Action.Rooms;
using FluentValidation;
using Models;

namespace Validation.Validators.Rooms
{
    public class CreateRoomValidator : AbstractValidator<Room>, IActionValidator<CreateRoom, Room>
    {
        public CreateRoomValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage(ValidationMessages.MissingLocation);
        }
    }
}
