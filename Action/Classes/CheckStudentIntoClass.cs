using Actions;
using Models;

namespace Action.Classes
{
    public class CheckStudentIntoClass : IAction<Class>
    {
        public Class ActionAgainst { get; set; }

        public CheckStudentIntoClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
