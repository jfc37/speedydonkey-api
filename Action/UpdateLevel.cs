using System;
using Actions;
using Models;

namespace Action
{
    public class UpdateLevel : ICrudAction<Level>
    {
        public UpdateLevel(Level level)
        {
            ActionAgainst = level;
        }

        public Level ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update level {0}", ActionAgainst.Name);
            }
        }
    }
}
