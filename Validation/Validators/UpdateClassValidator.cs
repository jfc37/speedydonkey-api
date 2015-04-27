using System;
using System.Collections.Generic;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateClassValidator : AbstractValidator<Class>, IActionValidator<UpdateClass, Class>
    {
        private readonly IRepository<Class> _repository;
        private readonly IRepository<User> _userRepository;

        public UpdateClassValidator(IRepository<Class> repository, IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
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
                var savedTeacher = _userRepository.Get(teacher.Id);
                if (savedTeacher == null || !savedTeacher.Claims.Contains(Claim.Teacher.ToString()) || savedTeacher.TeachingConcerns == null)
                    return false;
            }
            return true;
        }
    }
}
