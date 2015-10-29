using Actions;
using Models;

namespace Action.Classes
{
    public class UnassignClassRoom : IAction<Class>
    {
        public UnassignClassRoom(Class theClass)
        {
            ActionAgainst = theClass;
        }

        public Class ActionAgainst { get; set; }
    }
}