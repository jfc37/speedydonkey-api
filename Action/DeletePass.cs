using System;
using Actions;
using Models;

namespace Action
{
    public class DeletePass : ICrudAction<Pass>
    {
        public DeletePass(Pass pass)
        {
            ActionAgainst = pass;
        }

        public Pass ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Delete pass {0}", ActionAgainst.Id);
            }
        }
    }
}
