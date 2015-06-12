using System;
using Actions;
using Models;

namespace Action
{
    public class UpdateUserNote : ICrudAction<User>
    {
        public UpdateUserNote(User announcement)
        {
            ActionAgainst = announcement;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update user {0} with note {1}", ActionAgainst.FullName, ActionAgainst.Note);
            }
        }
    }
}
