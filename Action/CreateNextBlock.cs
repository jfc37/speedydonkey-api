using System;
using Actions;
using Models;

namespace Action
{
    public class CreateNextBlock : SystemAction<Block>, ICrudAction<Block>
    {
        public CreateNextBlock(Block block)
        {
            ActionAgainst = block;
        }
    }
}