using System;
using Action;
using Common;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class AddPassToUserValidator : AbstractValidator<User>, IActionValidator<AddPassToUser, User>
    {
        private readonly IRepository<User> _userRepository;
        private readonly ICurrentUser _currentUser;

        public AddPassToUserValidator(IRepository<User> userRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;

            RuleFor(x => x.Id)
                .Must(BeExistingUser).WithMessage(ValidationMessages.InvalidUser)
                .Must(HavePermissionToAddPass).WithMessage(ValidationMessages.CannotAddPassForAnother);

            RuleFor(x => x.Passes).NotEmpty().WithMessage(ValidationMessages.ProvidePasses);
        }

        private bool HavePermissionToAddPass(int id)
        {
            if (id == _currentUser.Id)
                return true;

            var user = _userRepository.Get(_currentUser.Id);
            return !String.IsNullOrWhiteSpace(user.Claims) && user.Claims.Contains(Claim.EnrolOtherIntoBlock.ToString());
        }

        private bool BeExistingUser(int id)
        {
            return _userRepository.Get(id) != null;
        }
    }
}
