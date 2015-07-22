namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyRegistrationStrategy : IItemStrategy
    {
        public IItemPriceStrategy GetPriceStrategy()
        {
            return new WindyLindyPriceStrategy();
        }

        public IItemValidationStrategy GetValidationStrategy()
        {
            return new WindyLindyValidationStrategy();
        }

        public IItemDescriptionStrategy GetDescriptionStrategy()
        {
            return new WindyLindyDescriptionStrategy();
        }
    }
}