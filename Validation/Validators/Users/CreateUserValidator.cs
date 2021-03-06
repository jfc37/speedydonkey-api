using System.Linq;
using Action.Users;
using Common.Extensions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<User>, IActionValidator<CreateUser, User>, IActionValidator<UpdateUser, User>
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

            RuleFor(x => x.AgreesToTerms)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Equal(true).WithMessage(ValidationMessages.TermsAndConditions);
        }

        private bool BeUnique(User user, string email)
        {
            return _userRepository.Queryable()
                .Where(x => x.Id != user.Id)
                .Where(x => x.Email == email)
                .NotAny();
        }
    }
}
