using System;
using Common.Extensions;
using Models;
using Models.OnlinePayments;
using OnlinePayments.ItemStrategies.WindyLindy;

namespace OnlinePayments.ItemStrategies
{
    public class ItemStrategyFactory : IItemStrategyFactory
    {
        private readonly ITypedItemStrategy<PassTemplate> _passStrategy;

        public ItemStrategyFactory(ITypedItemStrategy<PassTemplate> passStrategy)
        {
            _passStrategy = passStrategy;
        }

        public IItemStrategy GetStrategy(OnlinePayment onlinePayment)
        {
            switch (onlinePayment.ItemType)
            {
                case OnlinePaymentItem.WindyLindy:
                    return new WindyLindyStrategy(onlinePayment);

                case OnlinePaymentItem.Pass:
                    return _passStrategy;

                default:
                    throw new ArgumentException("Don't have any strategies for item type: {0}".FormatWith(onlinePayment.ItemType));
            }
        }
    }
}
