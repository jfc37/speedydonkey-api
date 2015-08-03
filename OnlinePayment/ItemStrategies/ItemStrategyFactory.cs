using System;
using Common.Extensions;
using Models;
using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies
{
    public class ItemStrategyFactory : IItemStrategyFactory
    {
        private readonly ITypedItemStrategy<PassTemplate> _passStrategy;
        private readonly ITypedItemStrategy<Registration> _windyLindyStrategy;

        public ItemStrategyFactory(
            ITypedItemStrategy<PassTemplate> passStrategy,
            ITypedItemStrategy<Registration> windyLindyStrategy)
        {
            _passStrategy = passStrategy;
            _windyLindyStrategy = windyLindyStrategy;
        }

        public IItemStrategy GetStrategy(OnlinePayment onlinePayment)
        {
            switch (onlinePayment.ItemType)
            {
                case OnlinePaymentItem.WindyLindy:
                    return _windyLindyStrategy;

                case OnlinePaymentItem.Pass:
                    return _passStrategy;

                default:
                    throw new ArgumentException("Don't have any strategies for item type: {0}".FormatWith(onlinePayment.ItemType));
            }
        }
    }
}
