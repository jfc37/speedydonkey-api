using System;
using Common;
using Models;
using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies
{
    public class ItemValidationStrategyFactory : IItemValidationStrategyFactory
    {
        private readonly ITypedItemValidationStrategy<Registration> _windyLindyValidation;

        public ItemValidationStrategyFactory(ITypedItemValidationStrategy<Registration> windyLindyValidation)
        {
            _windyLindyValidation = windyLindyValidation;
        }

        public IItemValidationStrategy GetStrategy(OnlinePaymentItem itemType)
        {
            switch (itemType)
            {
                case OnlinePaymentItem.WindyLindy:
                    return _windyLindyValidation;

                default:
                    throw new ArgumentException("Don't have validation strategy for {0}".FormatWith(itemType));
            }
        }
    }
}