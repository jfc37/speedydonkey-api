using System;
using Common;
using Models.OnlinePayments;
using OnlinePayments.ItemStrategies.WindyLindy;

namespace OnlinePayments.ItemStrategies
{
    public class ItemStrategyFactory : IItemStrategyFactory
    {
        public IItemStrategy GetStrategy(OnlinePayment onlinePayment)
        {
            switch (onlinePayment.ItemType)
            {
                case OnlinePaymentItem.WindyLindy:
                    return new WindyLindyRegistrationStrategy(onlinePayment);

                default:
                    throw new ArgumentException("Don't have any strategies for item type: {0}".FormatWith(onlinePayment.ItemType));
            }
        }
    }
}
