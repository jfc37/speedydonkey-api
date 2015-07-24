namespace OnlinePayments.ItemStrategies
{
    public interface IItemValidationStrategy
    {
        bool IsValid(string itemId);
    }
}