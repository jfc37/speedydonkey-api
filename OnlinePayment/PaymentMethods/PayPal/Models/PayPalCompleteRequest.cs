using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.PayPal.Models
{
    public class PayPalCompleteRequest
    {
        public PayPalCompleteRequest(PayPalPayment onlinePayment)
        {
            Token = onlinePayment.Token;
            PayerId = onlinePayment.PayerId;
            Amount = onlinePayment.Total;
        }

        public PayPalCompleteRequest() { }

        public string Token { get; private set; }
        public decimal Amount { get; private set; }
        public string PayerId { get; private set; }
    }
}