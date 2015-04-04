using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class ActivateUserValidator : AbstractValidator<User>, IActionValidator<ActivateUser, User>
    {
        private readonly IRepository<User> _repository;

        public ActivateUserValidator(IRepository<User> repository)
        {
            _repository = repository;
            RuleFor(x => x.ActivationKey).Must(MatchExistingUserKey).WithMessage(ValidationMessages.BadActivationKey);
        }

        private bool MatchExistingUserKey(Guid key)
        {
            return _repository.GetAll().Any(x => x.ActivationKey == key);
        }
    }
}
