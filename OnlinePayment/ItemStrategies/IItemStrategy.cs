namespace OnlinePayments.ItemStrategies
{
    public interface IItemStrategy
    {
        IItemPriceStrategy GetPriceStrategy();
        IItemValidationStrategy GetValidationStrategy();
        IItemDescriptionStrategy GetDescriptionStrategy();
    }
}