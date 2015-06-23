using System.Linq;
using Action.OnlinePayment;
using Common;
using Data.Repositories;
using Models;
using OnlinePayment;
using OnlinePayment.Models;

namespace ActionHandlers.OnlinePayments
{
    public class ConfirmOnlinePaymentHandler : IActionHandlerWithResult<ConfirmOnlinePayment, PendingOnlinePayment, GetExpressCheckoutResponse>
    {
        private readonly IExpressCheckout _expressCheckout;
        private readonly IPaymentDetailsRetriever _paymentDetailsRetriever;
        private readonly IRepository<PendingOnlinePayment> _repository;
        private readonly ICurrentUser _currentUser;

        public ConfirmOnlinePaymentHandler(
            IExpressCheckout expressCheckout, 
            IPaymentDetailsRetriever paymentDetailsRetriever,
            IRepository<PendingOnlinePayment> repository,
            ICurrentUser currentUser)
        {
            _expressCheckout = expressCheckout;
            _paymentDetailsRetriever = paymentDetailsRetriever;
            _repository = repository;
            _currentUser = currentUser;
        }

        public GetExpressCheckoutResponse Handle(ConfirmOnlinePayment action)
        {
            var result = _expressCheckout.Get(action.ActionAgainst.Token);

            var pendingOnlinePayment = _repository.GetAll().Single(x => x.Token == action.ActionAgainst.Token);
            pendingOnlinePayment.PayerId = result.PayerId;
            _repository.Update(pendingOnlinePayment);

            result.Description = pendingOnlinePayment.Description;
            return result;
        }
    }
}
