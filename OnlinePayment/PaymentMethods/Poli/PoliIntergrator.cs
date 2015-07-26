using System;
using System.IO;
using System.Net;
using Common;
using Common.Extensions;
using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli
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
                NotificationURL = "{0}/online-payment/poli/nudge".FormatWith(appSettings.GetApiUrl())
            };

            var requestAsJson = request.ToJson();

            return requestAsJson;
        }
    }

    public interface IPoliIntergrator
    {
        StartPoliPaymentResponse InitiateTransaction(PoliPayment payment);
    }
    public class PoliIntergrator : IPoliIntergrator
    {
        private readonly IAppSettings _appSettings;

        public PoliIntergrator(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public StartPoliPaymentResponse InitiateTransaction(PoliPayment payment)
        {
            var json = System.Text.Encoding.UTF8.GetBytes(payment.ToInitiateTransactionRequest(_appSettings));
            
            var myRequest = CreateWebRequest(json);
            SendRequest(myRequest, json);

            var poliResponse = new StartPoliPaymentResponse();
            HttpWebResponse response = null;
            Stream data = null;
            StreamReader streamRead = null;
            try
            {
                response = (HttpWebResponse)myRequest.GetResponse();
                data = response.GetResponseStream();
                streamRead = new StreamReader(data);
                Char[] readBuff = new Char[response.ContentLength];
                int count = streamRead.Read(readBuff, 0, (int)response.ContentLength);
                while (count > 0)
                {
                    var outputData = new String(readBuff, 0, count);
                    Console.Write(outputData);
                    count = streamRead.Read(readBuff, 0, (int)response.ContentLength);
                    dynamic latest = Newtonsoft.Json.JsonConvert.DeserializeObject(outputData);
                    poliResponse = new StartPoliPaymentResponse(latest);
                }
            }
            finally
            {
                if (response.IsNotNull())
                    response.Close();
                if (data.IsNotNull())
                    data.Close();
                if (streamRead.IsNotNull())
                    streamRead.Close();   
            }

            return poliResponse;
        }

        private static void SendRequest(WebRequest myRequest, byte[] json)
        {
            Stream dataStream = myRequest.GetRequestStream();
            dataStream.Write(json, 0, json.Length);
            dataStream.Close();
        }

        private WebRequest CreateWebRequest(byte[] json)
        {
            var auth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_appSettings.GetSetting(AppSettingKey.PoliAuthorisation)));
            var myRequest = WebRequest.Create(_appSettings.GetSetting(AppSettingKey.PoliInitiateUrl));
            myRequest.Method = "POST";
            myRequest.ContentType = "application/json";
            myRequest.Headers.Add("Authorization", "Basic " + auth);
            myRequest.ContentLength = json.Length;
            return myRequest;
        }
    }
}
