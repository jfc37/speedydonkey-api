using System;
using Actions;
using Models;

namespace Action
{
    public class UpdateClass : ICrudAction<Class>
    {
        public UpdateClass(Class theClass)
        {
            ActionAgainst = theClass;
        }

        public Class ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update class {0}", ActionAgainst.Id);
            }
        }
    }
}
