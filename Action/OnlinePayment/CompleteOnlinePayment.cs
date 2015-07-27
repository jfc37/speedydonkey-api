using Actions;
using Common;
using Common.Extensions;
using Models;

namespace Action.OnlinePayment
{
    public class CompleteOnlinePayment : IAction<PendingOnlinePayment>
    {
        public CompleteOnlinePayment(string token)
        {
            ActionAgainst = new PendingOnlinePayment
            {
                Token = token
            };
        }
        public PendingOnlinePayment ActionAgainst { get; set; }
        public string LogText { get { return "Complete online payment with token {0}".FormatWith(ActionAgainst.Token); } }
    }
}
