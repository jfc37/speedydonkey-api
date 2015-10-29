using Actions;
using Models;

namespace Action.Blocks
{
    public class UnassignBlockRoom : IAction<Block>
    {
        public UnassignBlockRoom(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
    }
}