using System;
using Models;

namespace Actions
{
    public class SetAsTeacher : ICrudAction<Teacher>
    {
        public SetAsTeacher(Teacher user)
        {
            ActionAgainst = user;
        }

        public Teacher ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Set user {0} as teacher", ActionAgainst.FullName);
            }
        }
    }
}
