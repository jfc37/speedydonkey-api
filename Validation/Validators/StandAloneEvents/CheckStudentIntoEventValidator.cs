using System.Collections.Generic;
using System.Linq;
using Action.StandAloneEvents;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.StandAloneEvents
{
    public class CheckStudentIntoEventValidator : AbstractValidator<Event>, IActionValidator<CheckStudentIntoEvent, Event>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Event> _eventRepository;

        public CheckStudentIntoEventValidator(IRepository<User> userRepository, IRepository<Event> eventRepository)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id).Must(x => new DoesIdExist<Event>(eventRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidEvent);
            RuleFor(x => x.ActualStudents)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(x => new HasExactlyOneInSetRule(x).IsValid()).WithMessage(ValidationMessages.IncorrectNumberOfAttendees)
                .Must(BeExistingUser).WithMessage(ValidationMessages.InvalidUser)
                .Must(NotAlreadyBeAttendingEvent).WithMessage(ValidationMessages.AlreadyAttending);
        }

        private bool NotAlreadyBeAttendingEvent(Event theEvent, ICollection<User> users)
        {
            var retrievedClass = GetEvent(theEvent.Id);
            if (retrievedClass == null)
                return true;
            var userId = users.Single().Id;
            return retrievedClass.ActualStudents == null || retrievedClass.ActualStudents.All(x => x.Id != userId);
        }

        private bool BeExistingUser(ICollection<User> users)
        {
            var user = GetUser(users);
            return user != null;
        }

        private User GetUser(ICollection<User> users)
        {
            var user = _userRepository.Get(users.Single().Id);
            return user;
        }

        private Event GetEvent(int id)
        {
            var theClass = _eventRepository.Get(id);
            return theClass;
        }
    }
}