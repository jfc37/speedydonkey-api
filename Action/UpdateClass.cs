using Actions;
using Models;

namespace Action
{
    public class UpdateClass : SystemAction<Class>, ICrudAction<Class>
    {
        public UpdateClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
