using System.Collections.Generic;
using System.Linq;
using Action.StandAloneEvents;
using Data.Repositories;
using Models;

namespace ActionHandlers.StandAloneEvents
{
    public class CheckStudentIntoEventHandler : IActionHandler<CheckStudentIntoEvent, Event>
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<User> _userRepository;

        public CheckStudentIntoEventHandler(IRepository<Event> eventRepository, IRepository<User> userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public Event Handle(CheckStudentIntoEvent action)
        {
            var user = _userRepository.Get(action.ActionAgainst.ActualStudents.Single().Id);
            var theEvent = _eventRepository.Get(action.ActionAgainst.Id);
            AddStudentToClassAttendance(theEvent, user);
            _eventRepository.Update(theEvent);

            return theEvent;
        }

        private static void AddStudentToClassAttendance(Event theEvent, User user)
        {
            theEvent.ActualStudents = theEvent.ActualStudents ?? new List<User>();
            theEvent.ActualStudents.Add(user);
        }
    }
}