using System.Collections.Generic;

namespace Contracts
{
    public interface IEntityView<in TEntity, out TModel>
    {
        IEnumerable<TModel> ConvertFromEntity(TEntity user);
    }
}