using System.Linq;
using Data.Repositories;
using FluentValidation;
using Models;
using Models.OnlinePayments;
using Validation.Rules;

namespace Validation.Validators.OnlinePayments
{
    public class WindyLindyRegistrationValidator : AbstractValidator<OnlinePayment>, IWindyLindyRegistrationValidator
    {
        public WindyLindyRegistrationValidator(IRepository<Registration> repository)
        {
            RuleFor(x => x.ItemId).Must(x => new IsStringAValidGuidRule(x).IsValid());

        }
    }
}
