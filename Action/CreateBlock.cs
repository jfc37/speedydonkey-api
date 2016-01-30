using Actions;
using Models;

namespace Action
{
    public class CreateBlock : SystemAction<Block>, ICrudAction<Block>
    {
        public CreateBlock(Block block)
        {
            ActionAgainst = block;
        }
    }
}
