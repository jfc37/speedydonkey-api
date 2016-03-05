using System.Linq;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class ForgottenPasswordValidator : AbstractValidator<User>, IActionValidator<ForgottenPassword, User>
    {
        private readonly IRepository<User> _repository;

        public ForgottenPasswordValidator(IRepository<User> repository)
        {
            _repository = repository;

            RuleFor(x => x.Email).Must(ExistForActiveUser).WithMessage(ValidationMessages.InvalidUser);
        }

        private bool ExistForActiveUser(string email)
        {
            return _repository.Queryable().Any(x => x.Email == email && x.Status == UserStatus.Active);
        }
    }
}
