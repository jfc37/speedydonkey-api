using Action.Teachers;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.RuleRunners;
using Validation.Rules;

namespace Validation.Validators.Teachers
{
    /// <summary>
    /// Validator for setting a user as a teacher
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Models.Teacher}" />
    /// <seealso cref="Validation.Validators.IActionValidator{Action.Teachers.SetAsTeacher, Models.Teacher}" />
    public class SetAsTeacherValidator : AbstractValidator<Teacher>, IActionValidator<SetAsTeacher, Teacher>
    {
        public SetAsTeacherValidator(IRepository<Teacher> repository, IRepository<User> userRepository)
        {
            RuleFor(x => x.User.Id)
                .Must(x => new DoesIdExist<User>(userRepository, x).IsValid()).WithMessage(ValidationMessages.ItemDoesntExist);

            RuleFor(x => x.User.Id)
                .Must(x => new IsUserNotATeacher(repository, x).IsValid())
                .WithMessage(ValidationMessages.UserIsAlreadyATeacher);
        }
    }
}