using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteBlock : ICrudAction<Block>
    {
        public DeleteBlock(Block block)
        {
            ActionAgainst = block;
        }

        public Block ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Delete block {0}", ActionAgainst.Id);
            }
        }
    }
}
