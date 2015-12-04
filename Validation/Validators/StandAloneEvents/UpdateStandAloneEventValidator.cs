using Action.StandAloneEvents;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.StandAloneEvents
{
    public class UpdateStandAloneEventValidator : AbstractValidator<StandAloneEvent>, IActionValidator<UpdateStandAloneEvent, StandAloneEvent>
    {
        private readonly IRepository<Teacher> _teacherRepository;

        public UpdateStandAloneEventValidator(IRepository<StandAloneEvent> repository, IRepository<Teacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage(ValidationMessages.MissingPrice);

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<StandAloneEvent>(repository, x).IsValid()).WithMessage(ValidationMessages.InvalidEvent);

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