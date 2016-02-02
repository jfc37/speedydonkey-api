using Action.PrivateLessons;
using Data.Repositories;
using FluentValidation;
using Models;
using Models.PrivateLessons;
using Validation.Rules;

namespace Validation.Validators.PrivateLessons
{
    /// <summary>
    /// Validator for setting teacher availability
    /// </summary>
    public class SetTeacherAvailabilityValidator : AbstractValidator<TeacherAvailability>, IActionValidator<SetTeacherAvailability, TeacherAvailability>
    {
        public SetTeacherAvailabilityValidator(IRepository<Teacher> repository)
        {
            RuleFor(x => x.Teacher)
                .Must(x => new DoesIdExist<Teacher>(repository, x.Id).IsValid());

            RuleForEach(x => x.Availabilities)
                .SetValidator(new SetOpeningHoursValidator());
        }
    }
}