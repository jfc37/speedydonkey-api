using System;
using System.Linq;
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
                return String.Format("Remove student {0} from class {1}", ActionAgainst.ActualStudents.Single().Id, ActionAgainst.Id);
            }
        }

        public RemoveStudentFromClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
