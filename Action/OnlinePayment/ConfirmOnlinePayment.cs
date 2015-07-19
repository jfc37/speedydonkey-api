using Actions;
using Common;
using Models;

namespace Action.OnlinePayment
{
    public class ConfirmOnlinePayment : IAction<PendingOnlinePayment>
    {
        public ConfirmOnlinePayment(string token)
        {
            ActionAgainst = new PendingOnlinePayment
            {
                Token = token
            };
        }
        public PendingOnlinePayment ActionAgainst { get; set; }
        public string LogText { get { return "Confirm online payment with token {0}".FormatWith(ActionAgainst.Token); } }
    }
}
