using FluentValidation;
using Models.OnlinePayments;

namespace Validation.Validators.OnlinePayments
{
    public interface IWindyLindyRegistrationValidator : IValidator<OnlinePayment> { }
}