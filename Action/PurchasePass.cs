using System;
using Actions;
using Models;

namespace Action
{
    public class PurchasePass : SystemAction<User>
    {
        public PurchasePass(User user)
        {
            ActionAgainst = user;
        }

        public int PassTemplateId { get; set; }
    }
}
