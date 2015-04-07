using System;
using Actions;
using Models;

namespace Action
{
    public class UpdatePass : ICrudAction<Pass>
    {
        public UpdatePass(Pass pass)
        {
            ActionAgainst = pass;
        }

        public Pass ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update pass {0}", ActionAgainst.Id);
            }
        }
    }
    public class UpdateClipPass : ICrudAction<ClipPass>
    {
        public UpdateClipPass(ClipPass pass)
        {
            ActionAgainst = pass;
        }

        public ClipPass ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update clip pass {0}", ActionAgainst.Id);
            }
        }
    }
}
