using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal.Models;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public interface IExpressCheckout
    {
        PayPalConfirmResponse Get(string token);
        PayPalCompleteResponse Do(PayPalCompleteRequest request);
        StartPayPalPaymentResponse Set(PayPalPayment payment);
    }
}