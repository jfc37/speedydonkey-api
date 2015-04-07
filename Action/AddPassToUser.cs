using System;
using Actions;
using Models;

namespace Action
{
    public class AddPassToUser : IAction<User>
    {
        public AddPassToUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Add pass to user {0}", ActionAgainst.Id);
            }
        }
    }
}
