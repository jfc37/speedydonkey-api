using System.Collections.Generic;
using OnlinePayments.Models;

namespace OnlinePayments.PaymentMethods.PayPal.Models
{
    public class PayPalConfirmResponse
    {
        public string Token { get; set; }
        public IEnumerable<PaypalError> Errors { get; set; }
        public string Status { get; set; }
        public string PayerId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}