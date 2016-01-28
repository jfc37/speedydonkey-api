using System;
using Actions;
using Models;

namespace Action
{
    public class GenerateBlocksForAllLevels : SystemAction<Block>, ICrudAction<Block>
    {
        public GenerateBlocksForAllLevels(Block block)
        {
            ActionAgainst = block;
        }
    }
}
