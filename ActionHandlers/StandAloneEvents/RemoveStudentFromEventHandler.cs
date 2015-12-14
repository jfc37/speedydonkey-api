using System.Collections.Generic;
using System.Linq;
using Action.StandAloneEvents;
using Data.Repositories;
using Models;

namespace ActionHandlers.StandAloneEvents
{
    public class RemoveStudentFromEventHandler : IActionHandler<RemoveStudentFromEvent, Event>
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<User> _userRepository;

        public RemoveStudentFromEventHandler(IRepository<Event> eventRepository, IRepository<User> userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public Event Handle(RemoveStudentFromEvent action)
        {
            var user = _userRepository.Get(action.ActionAgainst.ActualStudents.Single().Id);
            var theEvent = _eventRepository.Get(action.ActionAgainst.Id);
            theEvent.ActualStudents = theEvent.ActualStudents ?? new List<User>();
            theEvent.ActualStudents.Remove(user);
            _eventRepository.Update(theEvent);

            return theEvent;
        }
    }
}