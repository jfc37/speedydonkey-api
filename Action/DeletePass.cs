using System;
using Actions;
using Models;

namespace Action
{
    public class DeletePass : SystemAction<Pass>, ICrudAction<Pass>
    {
        public DeletePass(Pass pass)
        {
            ActionAgainst = pass;
        }
    }
}
