using Actions;
using Models;

namespace Action.Classes
{
    public class ChangeClassTeachers : SystemAction<Class>
    {
        public ChangeClassTeachers(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
