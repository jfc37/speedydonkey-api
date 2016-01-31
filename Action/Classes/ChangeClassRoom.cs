using Actions;
using Models;

namespace Action.Classes
{
    public class ChangeClassRoom : SystemAction<Class>
    {
        public ChangeClassRoom(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}