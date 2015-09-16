using System;
using Models;

namespace Actions
{
    public class RemoveAsTeacher : ICrudAction<Teacher>
    {
        public RemoveAsTeacher(Teacher user)
        {
            ActionAgainst = user;
        }

        public Teacher ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Remove teacher id {0} as teacher", ActionAgainst.Id);
            }
        }
    }
}
