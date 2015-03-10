using Actions;
using Models;

namespace Action
{
    public class CreateBlock : ICreateAction<Block>
    {
        public CreateBlock(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
    }
}
