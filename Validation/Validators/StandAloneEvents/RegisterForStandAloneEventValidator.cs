using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Common.Extensions;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.StandAloneEvents
{
    public class RegisterForStandAloneEventValidator : AbstractValidator<User>, IActionValidator<EnrolInBlock, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<StandAloneEvent> _eventRepository;
        private readonly ICurrentUser _currentUser;

        public RegisterForStandAloneEventValidator(IRepository<User> userRepository, IRepository<StandAloneEvent> eventRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _currentUser = currentUser;

            When(x => x.Schedule.IsNotNull(), () =>
            {
                RuleFor(x => x.Schedule)
                    .Must(NotAlreadyBeRegistered).WithMessage(ValidationMessages.AlreadyEnroledInBlock)
                    .Must(BeExistingStandAloneEvent).WithMessage(ValidationMessages.InvalidBlock)
                    .Must(ComplyWithInviteOnlyRule).WithMessage(ValidationMessages.UnavailableBlock);
                RuleFor(x => x.Id)
                    .Must(BeAllowedToEnrol).WithMessage(ValidationMessages.InvalidUserToEnrol);
            });
        }

        private bool ComplyWithInviteOnlyRule(ICollection<Event> events)
        {
            var isUserATeacher = IsUserATeacher();

            var blockIds = events.Select(x => x.Id);
            var isEnrollingInInviteOnly = _eventRepository.Queryable()
                .Where(x => blockIds.Contains(x.Id))
                .Any(x => x.IsPrivate);

            return !isEnrollingInInviteOnly || isUserATeacher;
        }

        private bool BeAllowedToEnrol(int userIdBeingEnroled)
        {
            if (userIdBeingEnroled == _currentUser.Id)
                return true;

            return IsUserATeacher();
        }

        private bool IsUserATeacher()
        {
            var user = _userRepository.Get(_currentUser.Id);
            return new DoesUserHaveClaimRule(user, Claim.Teacher)
                .IsValid();
        }

        private bool BeExistingStandAloneEvent(ICollection<Event> blocks)
        {
            return _eventRepository.Queryable()
                .Select(x => x.Id)
                .Intersect(blocks.Select(x => x.Id)).Count() == blocks.Count;
        }

        private bool NotAlreadyBeRegistered(User user, ICollection<Event> blocksBeingEnroledIn)
        {
            var alreadyEnroledBlockIds = _userRepository.Get(user.Id).Schedule.Select(x => x.Id);
            var enrolingBlockIds = blocksBeingEnroledIn.Select(x => x.Id);
            return !alreadyEnroledBlockIds.Intersect(enrolingBlockIds).Any();
        }
    }
}
