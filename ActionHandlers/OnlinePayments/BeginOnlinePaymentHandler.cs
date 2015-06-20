using Action.OnlinePayment;
using Data.Repositories;
using Models;
using OnlinePayment;
using OnlinePayment.Models;

namespace ActionHandlers.OnlinePayments
{
    public class BeginOnlinePaymentHandler : IActionHandler<BeginOnlinePayment, PendingOnlinePayment>
    {
        private readonly IExpressCheckout _expressCheckout;

        public BeginOnlinePaymentHandler(IExpressCheckout expressCheckout)
        {
            _expressCheckout = expressCheckout;
        }

        public PendingOnlinePayment Handle(BeginOnlinePayment action)
        {
            var result = _expressCheckout.Set(new PaymentDetails
            {
                Amount = 10,
                BuyerEmail = "placid.joe@gmail.com",
                CancelUrl = "https://spa-speedydonkey.azurewebsites.net/cancel",
                ReturnUrl = "https://spa-speedydonkey.azurewebsites.net/return",
                Description = "Pass"
            });
            return action.ActionAgainst;
        }
    }
}
