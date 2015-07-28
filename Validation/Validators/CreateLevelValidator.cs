using System;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public class CreateLevelValidator : AbstractValidator<Level>, IActionValidator<CreateLevel, Level>
    {
        private readonly IRepository<Teacher> _teacherRepository;

        public CreateLevelValidator(IRepository<Teacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;
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

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(x => new AreUsersExistingTeachersRule(x, _teacherRepository).IsValid()).WithMessage(ValidationMessages.InvalidTeachers);
        }
    }
}
