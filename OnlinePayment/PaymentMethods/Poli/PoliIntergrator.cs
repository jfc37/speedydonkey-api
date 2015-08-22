using System.IO;
using System.Net;
using System.Web;
using Common;
using Common.Extensions;
using Models.OnlinePayments;
using OnlinePayments.CodeChunks;
using OnlinePayments.PaymentMethods.Poli.Extensions;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli
{
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
            
            var myRequest = CreatePostRequest(json);
            SendRequest(myRequest, json);

            var poliResponse = new GetResponseFromHttpRequest<InitiatePoliTransaction>(myRequest).Do();

            return poliResponse.ToResponse();
        }

        public GetTransactionResponse GetTransaction(string token)
        {
            var myRequest = CreateGetRequest(token);

            var poliResponse = new GetResponseFromHttpRequest<GetTransactionResponse>(myRequest).Do();
            return poliResponse;
        }

        private WebRequest CreateGetRequest(string token)
        {
            var url = "{0}/GetTransaction?token={1}".FormatWith(_appSettings.GetSetting(AppSettingKey.PoliInitiateUrl), HttpUtility.UrlEncode(token));
            var request = new CreateWebRequestForPoli(_appSettings, url).Do();
            request.Method = "GET";
            
            return request;
        }

        private static void SendRequest(WebRequest myRequest, byte[] json)
        {
            Stream dataStream = myRequest.GetRequestStream();
            dataStream.Write(json, 0, json.Length);
            dataStream.Close();
        }

        private WebRequest CreatePostRequest(byte[] json)
        {
            var url = "{0}/Initiate".FormatWith(_appSettings.GetSetting(AppSettingKey.PoliInitiateUrl));
            var request = new CreateWebRequestForPoli(_appSettings, url).Do();

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = json.Length;
            return request;
        }
    }
}
