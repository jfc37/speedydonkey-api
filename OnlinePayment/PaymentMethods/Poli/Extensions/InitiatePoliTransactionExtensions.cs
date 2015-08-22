using System;
using System.Web;
using Common.Extensions;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli.Extensions
{
    public static class InitiatePoliTransactionExtensions
    {
        public static StartPoliPaymentResponse ToResponse(this InitiatePoliTransaction instance)
        {
            var response = new StartPoliPaymentResponse
            {
                PoliId = instance.TransactionRefNo,
                RedirectUrl = instance.NavigateURL,
                Token = GetTokenFromUrl(instance.NavigateURL)
            };

            if (instance.ErrorMessage.IsNotNullOrWhiteSpace())
            {
                response.AddError(instance.ErrorMessage);
            }

            return response;
        }



        private static string GetTokenFromUrl(string navigateUrl)
        {
            var uri = new Uri(navigateUrl);
            var query = HttpUtility.ParseQueryString(uri.Query);
            return query.Get("token");
        }
    }
}