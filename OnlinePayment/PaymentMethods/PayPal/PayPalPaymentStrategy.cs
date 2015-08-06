using Data.CodeChunks;
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
            payment.ReturnUrl = new ReferenceNumberPlaceholderReplacement(payment.ReturnUrl, payment.ReferenceNumber).Do();

            var response = _expressCheckout.Set(payment);
            payment.Token = response.Token;
            response.ReferenceNumber = payment.ReferenceNumber;

            return response;
        }
    }

}