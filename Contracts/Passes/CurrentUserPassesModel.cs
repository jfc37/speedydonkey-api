using System.Collections.Generic;
using System.Linq;
using Contracts.MappingExtensions;
using Models;

namespace Contracts.Passes
{
    public class CurrentUserPassesModel : IEntityView<User, PassModel>
    {
        public IEnumerable<PassModel> ConvertFromEntity(User user)
        {
            if (user.Passes == null)
                return new List<PassModel>();
            var validPasses = user.Passes.Where(x => x.IsValid());
            return validPasses.Select(x => x.ToModel());
        }
    }
}