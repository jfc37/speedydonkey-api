namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyPriceStrategy : IItemPriceStrategy
    {
        public decimal GetPrice(string itemId)
        {
            return new decimal(199.99);
        }
    }
}