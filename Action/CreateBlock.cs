using System;
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
        public string LogText
        {
            get
            {
                return String.Format("Create block for level {0}", ActionAgainst.Level.Id);
            }
        }
    }
}
