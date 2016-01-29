using Actions;
using Models;

namespace Action.Classes
{
    public class UnassignClassRoom : SystemAction<Class>
    {
        public UnassignClassRoom(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}