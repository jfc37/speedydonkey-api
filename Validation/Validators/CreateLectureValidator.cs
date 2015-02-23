using Actions;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateLectureValidator : AbstractValidator<Lecture>, IActionValidator<CreateLecture, Lecture>
    {
        public CreateLectureValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
            RuleFor(x => x.Occurence).NotEqual(Occurence.Invalid).WithMessage(ValidationMessages.MissingOccurence);
        }
    }
}
