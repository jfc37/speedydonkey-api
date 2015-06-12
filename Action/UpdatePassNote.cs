using System;
using Actions;
using Models;

namespace Action
{
    public class UpdatePassNote : ICrudAction<Pass>
    {
        public UpdatePassNote(Pass announcement)
        {
            ActionAgainst = announcement;
        }

        public Pass ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update pass {0} with note {1}", ActionAgainst.Id, ActionAgainst.Note);
            }
        }
    }
}
