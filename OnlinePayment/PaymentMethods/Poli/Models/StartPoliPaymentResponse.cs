using System;
using System.Collections.Generic;
using System.Web;
using Common;
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

        public StartPoliPaymentResponse(dynamic latest)
        {
            PoliId = latest.TransactionRefNo;
            RedirectUrl = latest.NavigateURL;
            Token = GetTokenFromUrl(RedirectUrl);

            if (latest.ErrorMessage.HasValues)
                Errors = latest.ErrorMessage.PutIntoList();
            else
                Errors = new List<string>();
        }

        private string GetTokenFromUrl(string navigateUrl)
        {
            var uri = new Uri(navigateUrl);
            var query = HttpUtility.ParseQueryString(uri.Query);
            return query.Get("token");
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
