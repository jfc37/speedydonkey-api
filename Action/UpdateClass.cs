using Actions;
using Models;

namespace Action
{
    public class UpdateClass : ICrudAction<Class>
    {
        public UpdateClass(Class theClass)
        {
            ActionAgainst = theClass;
        }

        public Class ActionAgainst { get; set; }
    }
}
