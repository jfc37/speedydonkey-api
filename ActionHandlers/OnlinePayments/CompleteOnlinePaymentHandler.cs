using System.Linq;
using Action;
using Action.OnlinePayment;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using OnlinePayments.PaymentMethods.PayPal;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace ActionHandlers.OnlinePayments
{
    public class CompleteOnlinePaymentHandler : IActionHandlerWithResult<CompleteOnlinePayment, PendingOnlinePayment, PayPalCompleteResponse>
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

        public PayPalCompleteResponse Handle(CompleteOnlinePayment action)
        {
            var pendingOnlinePayment = _repository.GetAll().Single(x => x.Token == action.ActionAgainst.Token);
            var request = pendingOnlinePayment.ToDoExpressCheckoutRequest();

            var result = _expressCheckout.Do(request);

            if (result.Errors.NotAny())
                pendingOnlinePayment.Status = OnlinePaymentStatus.Complete;

            _repository.Update(pendingOnlinePayment);

            _purchasePassHandler.Handle(new PurchasePass(new User(_currentUser.Id)
            {
                Passes = new Pass { PaymentStatus = PassPaymentStatus.Paid.ToString()}.ToList<IPass>()
            })
            {
                PassTemplateId = pendingOnlinePayment.TemplateId
            });

            return result;
        }
    }

    public static class PendingOnlinePaymentExtensions
    {
        public static PayPalCompleteRequest ToDoExpressCheckoutRequest(this PendingOnlinePayment instance)
        {
            return new PayPalCompleteRequest
            {
                Amount = instance.Amount,
                Token = instance.Token,
                PayerId = instance.PayerId
            };
        }
    }
}
