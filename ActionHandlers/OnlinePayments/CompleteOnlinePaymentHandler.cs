using System.Linq;
using Action;
using Action.OnlinePayment;
using Common;
using Data.Repositories;
using Models;
using OnlinePayment;
using OnlinePayment.Models;

namespace ActionHandlers.OnlinePayments
{
    public class CompleteOnlinePaymentHandler : IActionHandlerWithResult<CompleteOnlinePayment, PendingOnlinePayment, DoExpressCheckoutResponse>
    {
        private readonly IExpressCheckout _expressCheckout;
        private readonly IPaymentDetailsRetriever _paymentDetailsRetriever;
        private readonly IRepository<PendingOnlinePayment> _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IActionHandler<PurchasePass, User> _purchasePassHandler;

        public CompleteOnlinePaymentHandler(
            IExpressCheckout expressCheckout, 
            IPaymentDetailsRetriever paymentDetailsRetriever,
            IRepository<PendingOnlinePayment> repository,
            ICurrentUser currentUser,
            IActionHandler<PurchasePass, User> purchasePassHandler)
        {
            _expressCheckout = expressCheckout;
            _paymentDetailsRetriever = paymentDetailsRetriever;
            _repository = repository;
            _currentUser = currentUser;
            _purchasePassHandler = purchasePassHandler;
        }

        public DoExpressCheckoutResponse Handle(CompleteOnlinePayment action)
        {
            var pendingOnlinePayment = _repository.GetAll().Single(x => x.Token == action.ActionAgainst.Token);
            var request = pendingOnlinePayment.ToDoExpressCheckoutRequest();

            var result = _expressCheckout.Do(request);

            if (result.Errors.NotAny())
                pendingOnlinePayment.Status = OnlinePaymentStatus.Complete;

            _repository.Update(pendingOnlinePayment);

            _purchasePassHandler.Handle(new PurchasePass(new User(_currentUser.Id)
            {
                Passes = new[]
                {
                    new Pass
                    {
                        PaymentStatus = PassPaymentStatus.Paid.ToString()
                    },
                }
            })
            {
                PassTemplateId = pendingOnlinePayment.TemplateId
            });

            return result;
        }
    }

    public static class PendingOnlinePaymentExtensions
    {
        public static DoExpressCheckoutRequest ToDoExpressCheckoutRequest(this PendingOnlinePayment instance)
        {
            return new DoExpressCheckoutRequest
            {
                Amount = instance.Amount,
                Token = instance.Token,
                PayerId = instance.PayerId
            };
        }
    }
}
