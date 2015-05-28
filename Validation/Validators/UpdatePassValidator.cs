using System;
using Action;
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

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndTimeGreaterThanStartTime)
                .GreaterThan(DateTime.Now.AddYears(-10)).WithMessage(ValidationMessages.MissingEndTime);

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.Now.AddYears(-10)).WithMessage(ValidationMessages.MissingStartTime);

            RuleFor(x => x.Id)
                .Must(NotBeNegativeClassesRemaining).WithMessage(ValidationMessages.InvalidClipsRemaining);
        }

        private bool NotBeNegativeClassesRemaining(Pass pass, int id)
        {
            var clipPass = pass as IClipPass;
            if (clipPass == null)
                return true;

            return clipPass.ClipsRemaining >= 0;
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
