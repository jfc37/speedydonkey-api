using System;
using System.Linq;
using Actions;
using Models;

namespace Action
{
    public class CheckStudentIntoClass : IAction<Class>
    {
        public Class ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Check student {0} into class {1}", ActionAgainst.ActualStudents.Single().Id, ActionAgainst.Id);
            }
        }

        public CheckStudentIntoClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
