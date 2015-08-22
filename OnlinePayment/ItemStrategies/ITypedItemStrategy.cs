using Common;

namespace OnlinePayments.ItemStrategies
{
    public interface ITypedItemStrategy<TEntity> : IItemStrategy
        where TEntity : IEntity { }
}