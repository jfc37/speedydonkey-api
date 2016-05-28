using Action.StandAloneEvents;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.StandAloneEvents
{
    public class CreateStandAloneEventValidator : AbstractValidator<StandAloneEvent>, IActionValidator<CreateStandAloneEvent, StandAloneEvent>
    {
        private readonly IRepository<Teacher> _teacherRepository;

        public CreateStandAloneEventValidator(IRepository<Teacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage(ValidationMessages.MissingPrice);
            
            RuleFor(x => x.ClassCapacity)
                .GreaterThan(0).WithMessage(ValidationMessages.InvalidClassCapacity);
            
            RuleFor(x => x.TeacherRate)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.InvalidTeacherRate);

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