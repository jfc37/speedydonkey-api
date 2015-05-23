using System;
using Actions;
using Models;

namespace Action
{
    public class DeleteUser : ICrudAction<User>
    {
        public DeleteUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Delete user {0}", ActionAgainst.Id);
            }
        }
    }
}
