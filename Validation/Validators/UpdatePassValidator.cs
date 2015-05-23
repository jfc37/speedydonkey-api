using System.Linq;
using Action;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdatePassValidator : AbstractValidator<Pass>, IActionValidator<UpdatePass, Pass>
    {
        private readonly IRepository<Pass> _repository;
        private Pass _savedPass;

        public UpdatePassValidator(IRepository<Pass> repository)
        {
            _repository = repository;
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Id)
                .Must(BeExisting).WithMessage(ValidationMessages.InvalidPass);
        }

        private bool BeExisting(int id)
        {
            return GetSavedPass(id) != null;
        }

        private Pass GetSavedPass(int id)
        {
            if (_savedPass == null)
                _savedPass = _repository.Get(id);

            return _savedPass;
        }
    }
}
