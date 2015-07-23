using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyRegistrationStrategy : IItemStrategy
    {
        private readonly OnlinePayment _onlinePayment;

        public WindyLindyRegistrationStrategy(OnlinePayment onlinePayment)
        {
            _onlinePayment = onlinePayment;
        }

        public IItemPriceStrategy GetPriceStrategy()
        {
            return new WindyLindyPriceStrategy();
        }

        public IItemValidationStrategy GetValidationStrategy()
        {
            return new WindyLindyValidationStrategy(null);
        }

        public IItemDescriptionStrategy GetDescriptionStrategy()
        {
            return new WindyLindyDescriptionStrategy();
        }
    }
}