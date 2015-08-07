using System;
using System.Collections.Generic;
using Common.Extensions;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.Poli.Models
{
    public class StartPoliPaymentResponse : IStartOnlinePaymentResponse
    {
        public string RedirectUrl { get; set; }
        public string PoliId { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
        public bool IsValid { get { return Errors.NotAny(); } }
        public Guid ReferenceNumber { get; set; }

        public StartPoliPaymentResponse()
        {
            Errors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}
