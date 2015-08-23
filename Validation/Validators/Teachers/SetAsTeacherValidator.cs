using Actions;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.RuleRunners;

namespace Validation.Validators.Teachers
{
    public class SetAsTeacherValidator : PreExistingValidator<User>, IActionValidator<SetAsTeacher, User>
    {
        public SetAsTeacherValidator(IRepository<User> repository, IRepository<Teacher> teacherRepository)
            : base(repository)
        {
            RuleFor(x => x.Id)
                .Must(x => new IsUserNotATeacher(teacherRepository, x).IsValid())
                .WithMessage(ValidationMessages.UserIsAlreadyATeacher);
        }
    }
}