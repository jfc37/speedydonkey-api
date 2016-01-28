using Actions;
using Models;

namespace Action
{
    public class UpdateBlock : SystemAction<Block>, ICrudAction<Block>
    {
        public UpdateBlock(Block block)
        {
            ActionAgainst = block;
        }
    }
}
