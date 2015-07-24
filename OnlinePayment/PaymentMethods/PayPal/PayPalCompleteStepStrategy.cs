using System.Linq;
using Common;
using Data.Repositories;
using Models;
using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public class PayPalCompleteStepStrategy : IPaymentStepStrategy<string, PayPalCompleteResponse>
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

        public PayPalCompleteResponse PerformStep(string token)
        {
            var onlinePayment = _repository
                .GetAll()
                .Single(x => x.Token == token);
            var request = new PayPalCompleteRequest(onlinePayment);

            var result = _expressCheckout.Do(request);

            if (result.Errors.NotAny())
            {
                onlinePayment.PaymentStatus = OnlinePaymentStatus.Complete;
                _repository.Update(onlinePayment);
            }

            return result;
        }
    }
}