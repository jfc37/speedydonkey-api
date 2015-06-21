using System.Collections.Generic;

namespace OnlinePayment.Models
{
    public class SetExpressCheckoutResponse
    {
        public string Status { get; set; }
        public string Token { get; set; }
        public IEnumerable<PaypalError> Errors { get; set; }
    }
}