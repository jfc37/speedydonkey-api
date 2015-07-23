using System.Linq;
using Data.Repositories;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public class PayPalConfirmStepStrategy : IPaymentStepStrategy<string, PayPalConfirmResponse>
    {
        private readonly IExpressCheckout _expressCheckout;
        private readonly IRepository<PayPalPayment> _repository;

        public PayPalConfirmStepStrategy(
            IExpressCheckout expressCheckout,
            IRepository<PayPalPayment> repository)
        {
            _expressCheckout = expressCheckout;
            _repository = repository;
        }

        public PayPalConfirmResponse PerformStep(string token)
        {
            var result = _expressCheckout.Get(token);

            var onlinePayment = _repository.GetAll().Single(x => x.Token == token);

            result.Description = onlinePayment.Description;
            result.Amount = onlinePayment.Price;

            return result;
        }
    }
}