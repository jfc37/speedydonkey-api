using System.Linq;
using Action.OnlinePayment;
using Data.Repositories;
using Models;
using OnlinePayments;

namespace ActionHandlers.OnlinePayments
{
    public class ConfirmOnlinePaymentHandler : IActionHandlerWithResult<ConfirmOnlinePayment, PendingOnlinePayment, GetExpressCheckoutResponse>
    {
        private readonly IExpressCheckout _expressCheckout;
        private readonly IRepository<PendingOnlinePayment> _repository;

        public ConfirmOnlinePaymentHandler(
            IExpressCheckout expressCheckout,
            IRepository<PendingOnlinePayment> repository)
        {
            _expressCheckout = expressCheckout;
            _repository = repository;
        }

        public GetExpressCheckoutResponse Handle(ConfirmOnlinePayment action)
        {
            var result = _expressCheckout.Get(action.ActionAgainst.Token);

            var pendingOnlinePayment = _repository.GetAll().Single(x => x.Token == action.ActionAgainst.Token);
            pendingOnlinePayment.PayerId = result.PayerId;
            _repository.Update(pendingOnlinePayment);

            result.Description = pendingOnlinePayment.Description;
            result.Amount = pendingOnlinePayment.Amount;
            return result;
        }
    }
}
