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
                return String.Format("Remove user {0} as teacher", ActionAgainst.FullName);
            }
        }
    }
}
