using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteLevel : ICrudAction<Level>
    {
        public DeleteLevel(Level level)
        {
            ActionAgainst = level;
        }

        public Level ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Delete level {0}", ActionAgainst.Name);
            }
        }
    }
}
