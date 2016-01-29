using System;
using Actions;
using Models;

namespace Action
{
    public class UpdatePassNote : SystemAction<Pass>, ICrudAction<Pass>
    {
        public UpdatePassNote(Pass announcement)
        {
            ActionAgainst = announcement;
        }
    }
}
