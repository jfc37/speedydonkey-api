using Actions;
using Models;

namespace Action.Classes
{
    public class ChangeClassRoom : IAction<Class>
    {
        public ChangeClassRoom(Class theClass)
        {
            ActionAgainst = theClass;
        }

        public Class ActionAgainst { get; set; }
    }
}