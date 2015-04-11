using System;
using Models;

namespace Actions
{
    public class UpdateUser : ICrudAction<User>
    {
        public UpdateUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update user {0}", ActionAgainst.FullName);
            }
        }
    }
}
