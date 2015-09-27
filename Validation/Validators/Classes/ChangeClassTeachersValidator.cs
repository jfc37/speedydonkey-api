using Action.Classes;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Classes
{
    public class ChangeClassTeachersValidator : AbstractValidator<Class>, IActionValidator<ChangeClassTeachers, Class>
    {
        public ChangeClassTeachersValidator(IRepository<Class> classRepository, IRepository<Teacher> teacherRepository)
        {

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Class>(classRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidClass);

            RuleFor(x => x.Teachers)
                .NotEmpty().WithMessage(ValidationMessages.TeachersRequired)
                .Must(x => new AreTeachersValidRule(x, teacherRepository).IsValid()).WithMessage(ValidationMessages.InvalidTeachers);
        }
    }
}
