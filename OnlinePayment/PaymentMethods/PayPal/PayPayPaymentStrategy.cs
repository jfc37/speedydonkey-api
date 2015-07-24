using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public class PayPayPaymentStrategy : IStartPaymentStrategy<PayPalPayment, StartPayPalPaymentResponse>
    {
        private readonly IExpressCheckout _expressCheckout;

        public PayPayPaymentStrategy(IExpressCheckout expressCheckout)
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