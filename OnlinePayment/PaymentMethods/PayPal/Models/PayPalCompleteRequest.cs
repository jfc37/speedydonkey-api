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

        public string Token { get; set; }
        public decimal Amount { get; set; }
        public string PayerId { get; set; }
    }
}