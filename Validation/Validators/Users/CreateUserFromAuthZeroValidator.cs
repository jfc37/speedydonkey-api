using Action.Users;
using Common.Extensions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators.Users
{
    public class CreateUserFromAuthZeroValidator : AbstractValidator<User>, IActionValidator<CreateUserFromAuthZero, User>
    {
        private readonly IRepository<User> _repository;

        public CreateUserFromAuthZeroValidator(IRepository<User> repository)
        {
            _repository = repository;

            RuleFor(x => x.GlobalId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationMessages.MissingGlobalId)
                .Must(NotExists).WithMessage(ValidationMessages.AuthUserAlreadyExistsAsUser);
        }

        private bool NotExists(string globalId)
        {
            return _repository.Queryable()
                .NotAny(x => x.GlobalId == globalId);
        }
    }
}