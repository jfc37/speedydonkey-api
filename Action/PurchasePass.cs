using System;
using Actions;
using Models;

namespace Action
{
    public class PurchasePass : IAction<User>
    {
        public PurchasePass(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
        public int PassTemplateId { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Purchase pass for user {0}", ActionAgainst.Id);
            }
        }
    }
}
