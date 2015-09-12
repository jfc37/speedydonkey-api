using System.Collections.Generic;
using System.Linq;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class CurrentUserPassesModel : IEntityView<User, PassModel>
    {
        public IEnumerable<PassModel> ConvertFromEntity(User user)
        {
            if (user.Passes == null)
                return new List<PassModel>();
            var validPasses = user.Passes.Where(x => x.IsValid() || x.IsFuturePass());
            return validPasses.Select(x => x.ToModel());
        }
    }

    public interface IEntityView<in TEntity, out TModel>
    {
        IEnumerable<TModel> ConvertFromEntity(TEntity user);
    }
}