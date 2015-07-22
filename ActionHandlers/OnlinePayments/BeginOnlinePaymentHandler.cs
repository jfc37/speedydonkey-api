using Action.OnlinePayment;
using ActionHandlers.Calculations;
using Common;
using Data.Repositories;
using Models;
using OnlinePayments;
using OnlinePayments.Models;

namespace ActionHandlers.OnlinePayments
{
    public class BeginOnlinePaymentHandler : IActionHandlerWithResult<BeginOnlinePayment, PendingOnlinePayment, StartPayPalPaymentResponse>
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

        public StartPayPalPaymentResponse Handle(BeginOnlinePayment action)
        {
            var paymentDetails = _paymentDetailsRetriever.GetDetails(action);
            
            var result = _expressCheckout.Set(paymentDetails);

            action.ActionAgainst.Token = result.Token;
            action.ActionAgainst.UserId = _currentUser.Id;
            action.ActionAgainst.Amount = paymentDetails.Amount;
            action.ActionAgainst.Description = paymentDetails.Description;
            _repository.Create(action.ActionAgainst);
            return result;
        }
    }

    public interface IPaymentDetailsRetriever
    {
        PaymentDetails GetDetails(BeginOnlinePayment beginOnlinePayment);
    }

    public class PaymentDetailsRetriever : IPaymentDetailsRetriever
    {
        private readonly IRepository<PassTemplate> _repository;

        public PaymentDetailsRetriever(IRepository<PassTemplate> repository)
        {
            _repository = repository;
        }

        public PaymentDetails GetDetails(BeginOnlinePayment beginOnlinePayment)
        {
            var template = _repository.Get(beginOnlinePayment.ActionAgainst.TemplateId);
            var paymentTotal = new ExpressCheckoutTotalCalculation(template.Cost)
                .Calculate()
                .Result();
            return new PaymentDetails
            {
                Amount = paymentTotal,
                BuyerEmail = "placid.joe@gmail.com",
                CancelUrl = beginOnlinePayment.CancelUrl,
                ReturnUrl = beginOnlinePayment.ReturnUrl,
                Description = template.Description
            };
        }
    }
}
