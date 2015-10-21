using Actions;
using Models;

namespace Action.Classes
{
    public class ChangeClassTeachers : IAction<Class>
    {
        public ChangeClassTeachers(Class theClass)
        {
            ActionAgainst = theClass;
        }

        public Class ActionAgainst { get; set; }
    }
}
