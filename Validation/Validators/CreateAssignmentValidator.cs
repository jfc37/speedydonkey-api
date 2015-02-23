using System;
using Actions;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateAssignmentValidator : AbstractValidator<Assignment>, IActionValidator<CreateAssignment, Assignment>
    {
        public CreateAssignmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
            RuleFor(x => x.FinalMarkPercentage).LessThanOrEqualTo(100).WithMessage(ValidationMessages.FinalMarkPercentageGreaterThan100);
            RuleFor(x => x.StartDate).GreaterThan(DateTime.Today.AddYears(-1)).WithMessage(ValidationMessages.MissingStartDate);
            RuleFor(x => x.GradeType).NotEqual(GradeType.Invalid).WithMessage(ValidationMessages.MissingGradeType);
        }
    }
}
