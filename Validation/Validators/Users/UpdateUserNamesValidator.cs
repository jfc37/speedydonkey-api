using Action.Users;
using Data.Repositories;
using FluentValidation;
using Validation.Rules;

namespace Validation.Validators.Users
{
    public class UpdateUserNamesValidator : AbstractValidator<Models.User>, IActionValidator<UpdateUserNames, Models.User>
    {
        public UpdateUserNamesValidator(IRepository<Models.User> userRepository)
        {
            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Models.User>(userRepository, x).IsValid())
                .WithMessage(ValidationMessages.InvalidUser);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ValidationMessages.MissingFirstName);

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage(ValidationMessages.MissingSurname);
        }
    }
}