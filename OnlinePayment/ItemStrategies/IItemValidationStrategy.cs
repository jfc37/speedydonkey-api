using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies
{
    public interface IItemValidationStrategy
    {
        bool IsValid(OnlinePayment onlinePayment);
    }
}