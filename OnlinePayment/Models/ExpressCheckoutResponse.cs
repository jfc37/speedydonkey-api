using System.Collections.Generic;

namespace OnlinePayment.Models
{
    public class ExpressCheckoutResponse
    {
        public string Status { get; set; }
        public string Token { get; set; }
        public IEnumerable<PaypalError> Errors { get; set; }
    }
}