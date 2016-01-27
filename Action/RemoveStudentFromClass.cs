using System;
using Actions;
using Models;

namespace Action
{
    public class RemoveStudentFromClass : IAction<Class>
    {
        public Class ActionAgainst { get; set; }
        public string LogText { 
            get
            {
                return String.Format("Remove student from class {0}", ActionAgainst.Id);
            }
        }

        public RemoveStudentFromClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
