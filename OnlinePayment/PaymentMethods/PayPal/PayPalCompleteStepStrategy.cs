using System.Linq;
using Data.Repositories;
using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public class PayPalCompleteStepStrategy : ICompletePaymentStrategy<string, PayPalCompleteResponse, PayPalPayment>
    {
        private readonly IExpressCheckout _expressCheckout;
        private readonly IRepository<PayPalPayment> _repository;

        public PayPalCompleteStepStrategy(
            IExpressCheckout expressCheckout,
            IRepository<PayPalPayment> repository)
        {
            _expressCheckout = expressCheckout;
            _repository = repository;
        }

        public PayPalCompleteResponse CompletePayment(string token)
        {
            var onlinePayment = _repository
                .GetAll()
                .Single(x => x.Token == token);
            var request = new PayPalCompleteRequest(onlinePayment);

            var result = _expressCheckout.Do(request);

            return result;
        }

        public PayPalPayment GetCompletedPayment(string token)
        {
            var onlinePayment = _repository
                .GetAll()
                .Single(x => x.Token == token);
            return onlinePayment;
        }
    }
}