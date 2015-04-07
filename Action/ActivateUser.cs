using System;
using Actions;
using Models;

namespace Action
{
    public class ActivateUser : IAction<User>
    {
        public ActivateUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Activate user {0}", ActionAgainst.Id);
            }
        }
    }
}
