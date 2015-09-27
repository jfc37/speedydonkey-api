using Actions;
using Common.Extensions;
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
        public string LogText { get { return "Changing teachers for class {0}".FormatWith(ActionAgainst.Id); } }
    }
}
