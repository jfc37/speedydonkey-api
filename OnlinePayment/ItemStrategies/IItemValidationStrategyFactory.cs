using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies
{
    public interface IItemValidationStrategyFactory
    {
        IItemValidationStrategy GetStrategy(OnlinePaymentItem itemType);
    }
}