using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteBlock : SystemAction<Block>, ICrudAction<Block>
    {
        public DeleteBlock(Block block)
        {
            ActionAgainst = block;
        }
    }
}
