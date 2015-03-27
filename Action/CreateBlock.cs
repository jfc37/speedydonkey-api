using Actions;
using Models;

namespace Action
{
    public class CreateBlock : ICrudAction<Block>
    {
        public CreateBlock(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
    }
}
