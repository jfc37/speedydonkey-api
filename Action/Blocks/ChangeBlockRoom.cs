using Actions;
using Models;

namespace Action.Blocks
{
    public class ChangeBlockRoom : IAction<Block>
    {
        public ChangeBlockRoom(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
    }
}