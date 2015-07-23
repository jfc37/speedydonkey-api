using Data.Repositories;
using Models;
using Models.OnlinePayments;
using Validation.Validators.OnlinePayments;

namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyValidationStrategy : IItemValidationStrategy
    {
        private readonly IWindyLindyRegistrationValidator _validator;

        public WindyLindyValidationStrategy(IWindyLindyRegistrationValidator validator)
        {
            _validator = validator;
        }

        public bool IsValid(OnlinePayment onlinePayment)
        {
            return true;
            return _validator.Validate(onlinePayment)
                .IsValid;
        }
    }
}