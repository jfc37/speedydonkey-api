using System.Collections.Generic;
using System.Linq;
using Models.OnlinePayments;

namespace OnlinePayments.Models
{
    public class StartPayPalPaymentResponse : IStartOnlinePaymentResponse
    {
        public string Status { get; set; }
        public string Token { get; set; }
        public IEnumerable<PaypalError> Errors { get; set; }
        public bool IsValid { get { return !Errors.Any(); } }

        public StartPayPalPaymentResponse()
        {
            Errors = new List<PaypalError>();
        }
    }
}