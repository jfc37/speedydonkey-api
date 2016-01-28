using Actions;
using Models;

namespace Action.Blocks
{
    public class UnassignBlockRoom : SystemAction<Block>
    {
        public UnassignBlockRoom(Block block)
        {
            ActionAgainst = block;
        }
    }
}