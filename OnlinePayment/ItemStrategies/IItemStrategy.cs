using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies
{
    public interface IItemStrategy
    {
        decimal GetPrice(string itemId);
        string GetDescription(string itemId);
        void CompletePurchase(OnlinePayment completedPayment);
    }
}