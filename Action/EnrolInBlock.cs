using System;
using System.Linq;
using Actions;
using Models;

namespace Action
{
    public class EnrolInBlock : IAction<User>
    {
        public EnrolInBlock(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Enrol user {0} into block {1}", ActionAgainst.Id, ActionAgainst.EnroledBlocks.Single().Id);
            }
        }
    }
}
