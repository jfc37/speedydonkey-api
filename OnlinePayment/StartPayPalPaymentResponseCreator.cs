using OnlinePayments.PaymentMethods.PayPal.Models;

namespace OnlinePayments
{
    public class StartPayPalPaymentResponseCreator : IResponseCreator<StartPayPalPaymentResponse>
    {
        public StartPayPalPaymentResponse Create()
        {
            return new StartPayPalPaymentResponse();
        }
    }
}