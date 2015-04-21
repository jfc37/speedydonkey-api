using System;
using Actions;
using Models;

namespace Action
{
    public class UpdateBlock : ICrudAction<Block>
    {
        public UpdateBlock(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update block {0}", ActionAgainst.Id);
            }
        }
    }
}
