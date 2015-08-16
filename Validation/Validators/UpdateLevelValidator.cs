using Action;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public class UpdateLevelValidator : AbstractValidator<Level>, IActionValidator<UpdateLevel, Level>
    {
        public UpdateLevelValidator(IRepository<Level> repository, IRepository<Teacher> teacherRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.StartTime)
                .Must(x => new DateIsNotTooFarInThePastRule(x).IsValid()).WithMessage(ValidationMessages.MissingStartTime);

            RuleFor(x => x.EndTime)
                .Must(x => new DateIsNotTooFarInThePastRule(x).IsValid()).WithMessage(ValidationMessages.MissingEndTime)
                .GreaterThan(x => x.StartTime).WithMessage(ValidationMessages.EndTimeGreaterThanStartTime);

            RuleFor(x => x.ClassMinutes)
                .GreaterThan(0).WithMessage(ValidationMessages.InvalidClassMinutes);

            RuleFor(x => x.ClassesInBlock)
                .GreaterThan(0).WithMessage(ValidationMessages.InvalidClassesInBlock);

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Level>(repository, x).IsValid()).WithMessage(ValidationMessages.InvalidLevel);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(x => new AreUsersExistingTeachersRule(x, teacherRepository).IsValid()).WithMessage(ValidationMessages.InvalidTeachers);
        }
    }
}
