using Action;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public class UpdateClassValidator : AbstractValidator<Class>, IActionValidator<UpdateClass, Class>
    {
        private readonly IRepository<Class> _repository;
        private readonly IRepository<Teacher> _teacherRepository;

        public UpdateClassValidator(IRepository<Class> repository, IRepository<Teacher> teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Class>(repository, x).IsValid()).WithMessage(ValidationMessages.InvalidClass);

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage(ValidationMessages.EndTimeGreaterThanStartTime)
                .Must(x => new DateIsNotTooFarInThePastRule(x.Date).IsValid()).WithMessage(ValidationMessages.MissingEndTime);

            RuleFor(x => x.StartTime)
                .Must(x => new DateIsNotTooFarInThePastRule(x.Date).IsValid()).WithMessage(ValidationMessages.MissingStartTime);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(x => new AreTeachersValidRule(x, _teacherRepository).IsValid()).WithMessage(ValidationMessages.InvalidTeachers);
        }
    }
}
