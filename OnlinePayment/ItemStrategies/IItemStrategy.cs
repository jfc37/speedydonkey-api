namespace OnlinePayments.ItemStrategies
{
    public interface IItemStrategy
    {
        decimal GetPrice();
        string GetDescription();
    }
}