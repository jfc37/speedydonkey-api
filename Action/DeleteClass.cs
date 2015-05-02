using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteClass : ICrudAction<Class>
    {
        public DeleteClass(Class theClass)
        {
            ActionAgainst = theClass;
        }

        public Class ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Delete class {0}", ActionAgainst.Id);
            }
        }
    }
}
