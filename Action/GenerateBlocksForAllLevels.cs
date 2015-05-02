using System;
using Actions;
using Models;

namespace Action
{
    public class GenerateBlocksForAllLevels : ICrudAction<Block>
    {
        public GenerateBlocksForAllLevels(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Generate blocks for all levels");
            }
        }
    }
}
