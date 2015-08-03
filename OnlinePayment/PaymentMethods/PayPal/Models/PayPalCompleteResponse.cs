using System.Collections.Generic;
using Common.Extensions;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.PayPal.Models
{
    public class PayPalCompleteResponse : ICompleteOnlinePaymentResponse
    {
        public string Token { get; set; }
        public List<PaypalError> Errors { get; set; }
        public string Status { get; set; }
        public string PayerId { get; set; }

        public bool IsValid
        {
            get
            {
                return Errors.NotAny();
            }
        }

        public PayPalCompleteResponse()
        {
            Errors = new List<PaypalError>();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(new PaypalError(errorMessage));
        }
    }
}