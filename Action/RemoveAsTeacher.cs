using System;
using Models;

namespace Actions
{
    public class RemoveAsTeacher : ICrudAction<User>
    {
        public RemoveAsTeacher(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Remove user {0} as teacher", ActionAgainst.FullName);
            }
        }
    }
}
