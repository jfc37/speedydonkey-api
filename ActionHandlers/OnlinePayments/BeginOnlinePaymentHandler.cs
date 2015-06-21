using Action.OnlinePayment;
using Common;
using Data.Repositories;
using Models;
using OnlinePayment;
using OnlinePayment.Models;

namespace ActionHandlers.OnlinePayments
{
    public class BeginOnlinePaymentHandler : IActionHandlerWithResult<BeginOnlinePayment, PendingOnlinePayment, SetExpressCheckoutResponse>
    {
        private readonly IExpressCheckout _expressCheckout;
        private readonly IPaymentDetailsRetriever _paymentDetailsRetriever;
        private readonly IRepository<PendingOnlinePayment> _repository;
        private readonly ICurrentUser _currentUser;

        public BeginOnlinePaymentHandler(
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

        public SetExpressCheckoutResponse Handle(BeginOnlinePayment action)
        {
            var paymentDetails = _paymentDetailsRetriever.GetDetails(action.ActionAgainst);
            
            var result = _expressCheckout.Set(paymentDetails);

            action.ActionAgainst.Token = result.Token;
            action.ActionAgainst.UserId = _currentUser.Id;
            action.ActionAgainst.Amount = paymentDetails.Amount;
            _repository.Create(action.ActionAgainst);
            return result;
        }
    }

    public interface IPaymentDetailsRetriever : IAutoRegistered
    {
        PaymentDetails GetDetails(PendingOnlinePayment pendingOnlinePayment);
    }

    public class PaymentDetailsRetriever : IPaymentDetailsRetriever
    {
        private readonly IRepository<PassTemplate> _repository;

        public PaymentDetailsRetriever(IRepository<PassTemplate> repository)
        {
            _repository = repository;
        }

        public PaymentDetails GetDetails(PendingOnlinePayment pendingOnlinePayment)
        {
            var template = _repository.Get(pendingOnlinePayment.TemplateId);
            return new PaymentDetails
            {
                Amount = template.Cost,
                BuyerEmail = "placid.joe@gmail.com",
                CancelUrl = "https://spa-speedydonkey.azurewebsites.net/cancel",
                ReturnUrl = "https://spa-speedydonkey.azurewebsites.net/return",
                Description = "Pass"
            };
        }
    }
}
