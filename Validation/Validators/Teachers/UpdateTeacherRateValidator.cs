using Action.Teachers;
using Common.Extensions;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Teachers
{
    /// <summary>
    /// Validator for updating teacher rate
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Models.Teacher}" />
    /// <seealso cref="Validation.Validators.IActionValidator{Action.Teachers.UpdateTeacherRate, Models.Teacher}" />
    public class UpdateTeacherRateValidator : AbstractValidator<Teacher>, IActionValidator<UpdateTeacherRate, Teacher>
    {
        public UpdateTeacherRateValidator(IRepository<Teacher> repository)
        {
            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Teacher>(repository, x).IsValid()).WithMessage(ValidationMessages.ItemDoesntExist);

            RuleFor(x => x.Rate)
                .NotNull();

            When(x => x.Rate.IsNotNull(), () =>
            {
                RuleFor(y => y.Rate.SoloRate)
                    .GreaterThanOrEqualTo(0)
                    .LessThanOrEqualTo(10000);

                RuleFor(y => y.Rate.PartnerRate)
                    .GreaterThanOrEqualTo(0)
                    .LessThanOrEqualTo(10000);
            });
        }
    }
}