using System.Linq;
using Action.Users;
using Actions;
using Data.Repositories;
using FluentValidation;

namespace Validation.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<Models.User>, IActionValidator<CreateUser, Models.User>, IActionValidator<UpdateUser, Models.User>
    {
        private readonly IRepository<Models.User> _userRepository;

        public CreateUserValidator(IRepository<Models.User> userRepository)
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

        private bool BeUnique(Models.User user, string email)
        {
            return _userRepository.GetAll()
                .Where(x => x.Id != user.Id)
                .All(x => x.Email != email);
        }
    }
}
