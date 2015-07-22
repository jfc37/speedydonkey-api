namespace OnlinePayments.ItemStrategies
{
    public interface IItemPriceStrategy
    {
        decimal GetPrice(int itemId);
    }
}