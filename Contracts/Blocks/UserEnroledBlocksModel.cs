using System.Collections.Generic;
using System.Linq;
using Contracts.MappingExtensions;
using Models;

namespace Contracts.Blocks
{
    public class UserEnroledBlocksModel : IEntityView<User, BlockModel>
    {
        public IEnumerable<BlockModel> ConvertFromEntity(User user)
        {
            if (user.EnroledBlocks == null)
                return new List<BlockModel>();
            return user.EnroledBlocks.Select(x => x.ToStripedModel());
        }
    }
}