using System;
using Actions;
using Models;

namespace Action
{
    public class ForgottenPassword : IAction<User>
    {
        public ForgottenPassword(User user)
        {
            ActionAgainst = user;
        }
        public string LogText
        {
            get
            {
                return String.Format("Forgetten password for user {0}", ActionAgainst.Id);
            }
        }

        public User ActionAgainst { get; set; }
    }
}
