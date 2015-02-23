using System;
using Actions;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateExamValidator : AbstractValidator<Exam>, IActionValidator<CreateExam, Exam>
    {
        public CreateExamValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.FinalMarkPercentage).LessThanOrEqualTo(100).WithMessage(ValidationMessages.FinalMarkPercentageGreaterThan100);
            RuleFor(x => x.StartTime).GreaterThan(DateTime.Today.AddYears(-1)).WithMessage(ValidationMessages.MissingStartTime);
            RuleFor(x => x.GradeType).NotEqual(GradeType.Invalid).WithMessage(ValidationMessages.MissingGradeType);
        }
    }
}
