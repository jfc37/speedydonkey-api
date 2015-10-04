using System;
using Actions;
using Models;

namespace Action
{
    public class CreateNextBlock : ICrudAction<Block>
    {
        public CreateNextBlock(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Create next block");
            }
        }
    }
}