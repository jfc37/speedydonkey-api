using System;
using System.Linq;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class ResetPasswordValidator : AbstractValidator<User>, IActionValidator<ResetPassword, User>
    {
        private readonly IRepository<User> _repository;

        public ResetPasswordValidator(IRepository<User> repository)
        {
            _repository = repository;

            RuleFor(x => x.ActivationKey).Must(BeKeyForUser).WithMessage(ValidationMessages.BadPasswordResetKey);

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationMessages.MissingPassword)
                .Must(x => x.Length >= 7).WithMessage(ValidationMessages.PasswordTooShort);
        }

        private bool BeKeyForUser(Guid key)
        {
            return _repository.GetAll().Any(x => x.ActivationKey == key && x.Status == UserStatus.Active);
        }
    }
}
