using System;
using Actions;
using Models;

namespace Action
{
    public class CreateLevel : ICrudAction<Level>
    {
        public CreateLevel(Level level)
        {
            ActionAgainst = level;
        }

        public Level ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Create level {0}", ActionAgainst.Name);
            }
        }
    }
}
