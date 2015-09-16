using System;
using Models;

namespace Actions
{
    public class SetAsTeacher : ICrudAction<Teacher>
    {
        public SetAsTeacher(Teacher teacher)
        {
            ActionAgainst = teacher;
        }

        public Teacher ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Set user {0} as teacher", ActionAgainst.Id);
            }
        }
    }
}
