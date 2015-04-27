using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateLevelValidator : AbstractValidator<Level>, IActionValidator<UpdateLevel, Level>
    {
        private readonly IRepository<Level> _repository;
        private readonly IRepository<Teacher> _teacherRepository;

        public UpdateLevelValidator(IRepository<Level> repository, IRepository<Teacher> teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
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

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.InvalidLevel);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(BeExistingTeachers).WithMessage(ValidationMessages.InvalidTeachers);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }

        private bool BeExistingTeachers(IList<ITeacher> teachers)
        {
            foreach (var teacher in teachers)
            {
                var savedTeacher = _teacherRepository.Get(teacher.Id);
                if (savedTeacher == null || !savedTeacher.Claims.Contains(Claim.Teacher.ToString()))
                    return false;
            }
            return true;
        }
    }
}
