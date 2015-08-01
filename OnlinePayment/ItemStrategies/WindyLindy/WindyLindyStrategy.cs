using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyStrategy : IItemStrategy
    {
        private readonly OnlinePayment _onlinePayment;

        public WindyLindyStrategy(OnlinePayment onlinePayment)
        {
            _onlinePayment = onlinePayment;
        }

        public decimal GetPrice(string itemId)
        {
            return new decimal(199.99);
        }

        public string GetDescription(string itemId)
        {
            return "Full Pass";
        }
    }
}