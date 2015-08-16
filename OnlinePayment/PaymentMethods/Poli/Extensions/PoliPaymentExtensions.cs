using Common;
using Common.Extensions;
using Models.OnlinePayments;

namespace OnlinePayments.PaymentMethods.Poli.Extensions
{
    public static class PoliPaymentExtensions
    {
        public static string ToInitiateTransactionRequest(this PoliPayment instance, IAppSettings appSettings)
        {
            var request = new
            {
                Amount = instance.Total.ToCurrencyString(),
                CurrencyCode = "NZD",
                MerchantReference = "{0}{1}".FormatWith(instance.ItemType, instance.ItemId),
                MerchantHomepageURL = appSettings.GetWebsiteUrl(),
                SuccessURL = instance.SuccessUrl,
                FailureURL = instance.FailureUrl,
                CancellationURL = instance.CancellationUrl,
                NotificationURL = "{0}/api/online-payment/poli/nudge".FormatWith(appSettings.GetApiUrl())
            };

            var requestAsJson = request.ToJson();

            return requestAsJson;
        }
    }
}