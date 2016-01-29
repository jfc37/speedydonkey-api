using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteClass : SystemAction<Class>, ICrudAction<Class>
    {
        public DeleteClass(Class theClass)
        {
            ActionAgainst = theClass;
        }
    }
}
