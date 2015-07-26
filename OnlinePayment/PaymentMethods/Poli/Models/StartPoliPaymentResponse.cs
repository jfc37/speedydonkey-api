using System;
using System.Collections.Generic;
using Common;
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

        public StartPoliPaymentResponse(dynamic latest)
        {
            PoliId = latest.TransactionRefNo;
            RedirectUrl = latest.NavigateURL;

            if (latest.ErrorMessage.IsNotNullOrWhiteSpace())
                Errors = latest.ErrorMessage.PutIntoList();
        }

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
