using System;
using Actions;
using Models;

namespace Action
{
    public class ResetPassword : IAction<User>
    {
        public ResetPassword(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Reset password for user {0}", ActionAgainst.Id);
            }
        }
    }
}
