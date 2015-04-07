using System;
using Models;

namespace Actions
{
    public class CreateUser : ICrudAction<User>
    {
        public CreateUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Create user {0}", ActionAgainst.FullName);
            }
        }
    }
}
