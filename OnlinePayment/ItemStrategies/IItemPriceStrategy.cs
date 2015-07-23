namespace OnlinePayments.ItemStrategies
{
    public interface IItemPriceStrategy
    {
        decimal GetPrice(string itemId);
    }
}