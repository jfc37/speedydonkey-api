using System.Linq;
using Action.StandAloneEvents;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace ActionHandlers.StandAloneEvents
{
    public class RegisterForStandAloneEventHandler : IActionHandler<RegisterForStandAloneEvent, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<StandAloneEvent> _eventRepository;

        public RegisterForStandAloneEventHandler(
            IRepository<User> userRepository,
            IRepository<StandAloneEvent> eventRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public User Handle(RegisterForStandAloneEvent action)
        {
            var user = _userRepository.Get(action.ActionAgainst.Id);

            var eventIds = action.ActionAgainst.Schedule
                .Select(x => x.Id)
                .ToList();

            var eventsToRegisterFor = _eventRepository.GetAll()
                .Where(x => eventIds.Contains(x.Id))
                .ToList();

            foreach (var standAloneEvent in eventsToRegisterFor)
            {
                standAloneEvent.RegisteredStudents.Add(user);
                _eventRepository.Update(standAloneEvent);
            }

            user.Schedule.AddRange(eventsToRegisterFor);

            return _userRepository.Update(user);
        }
    }
}
