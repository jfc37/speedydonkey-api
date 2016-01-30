using Actions;
using Models;

namespace Action.Blocks
{
    public class ChangeBlockRoom : SystemAction<Block>
    {
        public ChangeBlockRoom(Block block)
        {
            ActionAgainst = block;
        }
    }
}