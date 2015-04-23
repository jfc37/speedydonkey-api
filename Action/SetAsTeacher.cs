using System;
using Models;

namespace Actions
{
    public class SetAsTeacher : ICrudAction<User>
    {
        public SetAsTeacher(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Set user {0} as teacher", ActionAgainst.FullName);
            }
        }
    }
}
