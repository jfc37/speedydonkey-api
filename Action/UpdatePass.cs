using System;
using Actions;
using Models;

namespace Action
{
    public class UpdatePass : SystemAction<Pass>, ICrudAction<Pass>
    {
        public UpdatePass(Pass pass)
        {
            ActionAgainst = pass;
        }
    }
    public class UpdateClipPass : SystemAction<ClipPass>, ICrudAction<ClipPass>
    {
        public UpdateClipPass(ClipPass pass)
        {
            ActionAgainst = pass;
        }
    }
}
