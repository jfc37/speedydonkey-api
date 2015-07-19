using Actions;
using Common;
using Models;

namespace Action.OnlinePayment
{
    public class BeginOnlinePayment : IAction<PendingOnlinePayment>
    {
        public BeginOnlinePayment(PendingOnlinePayment pendingOnlinePayment, string returnUrl, string cancelUrl)
        {
            ActionAgainst = pendingOnlinePayment;
            ReturnUrl = returnUrl;
            CancelUrl = cancelUrl;
        }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public PendingOnlinePayment ActionAgainst { get; set; }
        public string LogText { get { return "Begin online payment for user {0}".FormatWith(ActionAgainst.UserId); } }
    }
}
