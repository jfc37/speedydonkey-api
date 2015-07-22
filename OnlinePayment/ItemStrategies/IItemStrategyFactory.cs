using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies
{
    public interface IItemStrategyFactory
    {
        IItemStrategy GetStrategy(OnlinePaymentItem itemType);
    }
}