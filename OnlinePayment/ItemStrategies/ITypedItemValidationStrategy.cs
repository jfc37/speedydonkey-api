using Common;

namespace OnlinePayments.ItemStrategies
{
    public interface ITypedItemValidationStrategy<TEntity> : IItemValidationStrategy
        where TEntity : IEntity { }
}