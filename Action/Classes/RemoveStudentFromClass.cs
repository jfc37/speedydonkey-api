using Actions;
using Models;

namespace Action.Classes
{
    public class RemoveStudentFromClass : SystemAction<Class>
    {
        public RemoveStudentFromClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
