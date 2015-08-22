using System;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

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
                .Must(x => new DoesIdExist<Pass>(repository, x).IsValid()).WithMessage(ValidationMessages.InvalidPass);

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndTimeGreaterThanStartTime)
                .Must(x => new DateIsNotTooFarInThePastRule(x).IsValid()).WithMessage(ValidationMessages.MissingEndTime);

            RuleFor(x => x.StartDate)
                .Must(x => new DateIsNotTooFarInThePastRule(x).IsValid()).WithMessage(ValidationMessages.MissingStartTime);

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

        private Pass GetSavedPass(int id)
        {
            if (_savedPass == null)
                _savedPass = _repository.Get(id);

            return _savedPass;
        }
    }
}
