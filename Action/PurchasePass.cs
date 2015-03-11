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
    }
}
