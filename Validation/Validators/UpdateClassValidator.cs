using System;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateClassValidator : AbstractValidator<Class>, IActionValidator<UpdateClass, Class>
    {
        private readonly IRepository<Class> _repository;

        public UpdateClassValidator(IRepository<Class> repository)
        {
            _repository = repository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.InvalidClass);

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage(ValidationMessages.EndTimeGreaterThanStartTime)
                .GreaterThan(DateTime.Now.AddYears(-10)).WithMessage(ValidationMessages.MissingEndTime);

            RuleFor(x => x.StartTime)
                .GreaterThan(DateTime.Now.AddYears(-10)).WithMessage(ValidationMessages.MissingStartTime);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}
