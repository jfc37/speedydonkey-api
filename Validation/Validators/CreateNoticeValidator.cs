using Actions;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateNoticeValidator : AbstractValidator<Notice>, IActionValidator<CreateNotice, Notice>
    {
        public CreateNoticeValidator()
        {
            RuleFor(x => x.Message).NotEmpty().WithMessage(ValidationMessages.MissingNoticeMessage);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
        }
    }
}
