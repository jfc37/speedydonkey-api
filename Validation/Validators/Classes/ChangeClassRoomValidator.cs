using Action.Classes;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Classes
{
    public class ChangeClassRoomValidator : AbstractValidator<Class>, IActionValidator<ChangeClassRoom, Class>
    {
        public ChangeClassRoomValidator(IRepository<Event> eventRepository, IRepository<Room> roomRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Event>(eventRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidClass);

            RuleFor(x => x.Room)
                .NotEmpty().WithMessage(ValidationMessages.RoomRequired)
                .Must(x => new DoesIdExist<Room>(roomRepository, x.Id).IsValid()).WithMessage(ValidationMessages.InvalidRoom)
                .Must((x, y) => new IsRoomAvailable(roomRepository, eventRepository, x.Id, y.Id).IsValid()).WithMessage(ValidationMessages.InvalidRoom);
        }
    }
}