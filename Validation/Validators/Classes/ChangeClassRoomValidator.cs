using Action.Classes;
using Common.Extensions;
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

    public class IsRoomAvailable: IRule
    {
        private readonly IRepository<Room> _repository;
        private readonly IRepository<Event> _eventRepository;
        private readonly int _scheduledEventId;
        private readonly int _roomId;

        public IsRoomAvailable(IRepository<Room> repository, IRepository<Event> eventRepository, int scheduledEventId, int roomId)
        {
            _repository = repository;
            _eventRepository = eventRepository;
            _scheduledEventId = scheduledEventId;
            _roomId = roomId;
        }

        public bool IsValid()
        {
            var scheduledEvent = _eventRepository.Get(_scheduledEventId);

            var room = _repository.Get(_roomId);
            return room.Events.NotAny(x => x.StartTime.IsBetween(scheduledEvent.StartTime, scheduledEvent.EndTime) || x.EndTime.IsBetween(scheduledEvent.StartTime, scheduledEvent.EndTime));
        }
    }
}