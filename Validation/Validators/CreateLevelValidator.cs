using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateLevelValidator : AbstractValidator<Level>, IActionValidator<CreateLevel, Level>
    {
        private readonly IRepository<User> _userRepository;

        public CreateLevelValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.StartTime)
                .GreaterThan(DateTime.Now.AddYears(-10)).WithMessage(ValidationMessages.MissingStartTime);

            RuleFor(x => x.EndTime)
                .GreaterThan(DateTime.Now.AddYears(-10)).WithMessage(ValidationMessages.MissingEndTime)
                .GreaterThan(x => x.StartTime).WithMessage(ValidationMessages.EndTimeGreaterThanStartTime);

            RuleFor(x => x.ClassMinutes)
                .GreaterThan(0).WithMessage(ValidationMessages.InvalidClassMinutes);

            RuleFor(x => x.ClassesInBlock)
                .GreaterThan(0).WithMessage(ValidationMessages.InvalidClassesInBlock);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(BeExistingTeachers).WithMessage(ValidationMessages.InvalidTeachers);
        }

        private bool BeExistingTeachers(IList<ITeacher> teachers)
        {
            foreach (var teacher in teachers)
            {
                var savedTeacher = _userRepository.Get(teacher.Id);
                if (savedTeacher == null || !savedTeacher.Claims.Contains(Claim.Teacher.ToString()) || savedTeacher.TeachingConcerns == null)
                    return false;
            }
            return true;
        }
    }
}
