using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public class PayPalPaymentStrategy : IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse>
    {
        private readonly IExpressCheckout _expressCheckout;

        public PayPalPaymentStrategy(IExpressCheckout expressCheckout)
        {
            _expressCheckout = expressCheckout;
        }

        public StartPayPalPaymentResponse StartPayment(PayPalPayment payment)
        {
            var response = _expressCheckout.Set(payment);
            payment.Token = response.Token;

            return response;
        }
    }
}