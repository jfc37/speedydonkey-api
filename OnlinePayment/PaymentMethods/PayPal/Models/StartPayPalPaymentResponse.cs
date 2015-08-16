using System;
using System.Collections.Generic;
using System.Linq;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.PayPal.Models
{
    public class StartPayPalPaymentResponse : IStartOnlinePaymentResponse
    {
        public string Status { get; set; }
        public string Token { get; set; }
        public List<PaypalError> Errors { get; set; }
        public bool IsValid { get { return !Errors.Any(); } }
        public Guid ReferenceNumber { get; set; }

        public StartPayPalPaymentResponse()
        {
            Errors = new List<PaypalError>();
        }

        public void AddError(string errorMessage)
        {   
            Errors.Add(new PaypalError(errorMessage));
        }
    }
}