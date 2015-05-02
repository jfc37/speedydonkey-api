using System;
using Action;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class CreateUpdatePassTemplateValidator : AbstractValidator<PassTemplate>, IActionValidator<CreatePassTemplate, PassTemplate>, IActionValidator<UpdatePassTemplate, PassTemplate>
    {
        public CreateUpdatePassTemplateValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ValidationMessages.MissingDescription);

            RuleFor(x => x.PassType)
                .NotEmpty().WithMessage(ValidationMessages.MissingPassType)
                .Must(BeValidPassType).WithMessage(ValidationMessages.InvalidPassType);

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.NegativeNumber);

            RuleFor(x => x.WeeksValidFor)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.NegativeNumber);

            RuleFor(x => x.ClassesValidFor)
                .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.NegativeNumber);
        }

        private bool BeValidPassType(string passType)
        {
            PassType parsedPassType;

            if (Enum.TryParse(passType, true, out parsedPassType))
            {
                return parsedPassType != PassType.Invalid;
            }

            return false;
        }
    }
}
