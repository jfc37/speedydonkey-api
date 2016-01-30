using Actions;
using Models;

namespace Action.Classes
{
    public class CheckStudentIntoClass : SystemAction<Class>
    {
        public CheckStudentIntoClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
