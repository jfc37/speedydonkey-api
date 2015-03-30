using System.Linq;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateUserValidator : AbstractValidator<User>, IActionValidator<CreateUser, User>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationMessages.MissingEmail)
                .EmailAddress().WithMessage(ValidationMessages.InvalidEmail)
                .Must(BeUnique).WithMessage(ValidationMessages.DuplicateEmail);

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationMessages.MissingPassword)
                .Must(x => x.Length >= 7).WithMessage(ValidationMessages.PasswordTooShort);

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationMessages.MissingFirstName);

            RuleFor(x => x.Surname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationMessages.MissingSurname);
        }

        private bool BeUnique(string email)
        {
            return _userRepository.GetAll().All(x => x.Email != email);
        }
    }
}
