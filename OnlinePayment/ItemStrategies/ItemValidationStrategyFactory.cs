using System;
using Common.Extensions;
using Models;
using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies
{
    public class ItemValidationStrategyFactory : IItemValidationStrategyFactory
    {
        private readonly ITypedItemValidationStrategy<Registration> _windyLindyValidation;
        private readonly ITypedItemValidationStrategy<PassTemplate> _passValidation;

        public ItemValidationStrategyFactory(
            ITypedItemValidationStrategy<Registration> windyLindyValidation,
            ITypedItemValidationStrategy<PassTemplate> passValidation)
        {
            _windyLindyValidation = windyLindyValidation;
            _passValidation = passValidation;
        }

        public IItemValidationStrategy GetStrategy(OnlinePaymentItem itemType)
        {
            switch (itemType)
            {
                case OnlinePaymentItem.WindyLindy:
                    return _windyLindyValidation;

                case OnlinePaymentItem.Pass:
                    return _passValidation;

                default:
                    throw new ArgumentException("Don't have validation strategy for {0}".FormatWith(itemType));
            }
        }
    }
}