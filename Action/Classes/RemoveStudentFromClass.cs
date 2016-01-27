using Actions;
using Models;

namespace Action.Classes
{
    public class RemoveStudentFromClass : IAction<Class>
    {
        public Class ActionAgainst { get; set; }

        public RemoveStudentFromClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
