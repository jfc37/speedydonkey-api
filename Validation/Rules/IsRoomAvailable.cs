using Common.Extensions;
using Data.Repositories;
using Models;

namespace Validation.Rules
{
    public class IsRoomAvailable: IRule
    {
        private readonly Room _room;
        private readonly Event _scheduledEvent;

        public IsRoomAvailable(IRepository<Room> repository, IRepository<Event> eventRepository, int scheduledEventId, int roomId)
        {
            _scheduledEvent = eventRepository.Get(scheduledEventId);

            _room = repository.Get(roomId);
        }

        public IsRoomAvailable(Room room, Event theEvent)
        {
            _room = room;
            _scheduledEvent = theEvent;
        }

        public bool IsValid()
        {
            return _room.Events.NotAny(x => x.StartTime.IsBetween(_scheduledEvent.StartTime, _scheduledEvent.EndTime) || x.EndTime.IsBetween(_scheduledEvent.StartTime, _scheduledEvent.EndTime));
        }
    }
}