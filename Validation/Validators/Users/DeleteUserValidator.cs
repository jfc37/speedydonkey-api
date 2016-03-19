using Action;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Users
{
    public class DeleteUserValidator : AbstractValidator<User>, IActionValidator<DeleteUser, User>
    {
        public DeleteUserValidator(IRepository<User> userRepository)
        {
            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<User>(userRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidUser);
        }
    }
}