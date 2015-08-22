using Data.CodeChunks;
using OnlinePayments.PaymentMethods.PayPal;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.CodeChunks
{
    public class GetExpressCheckoutDetailsReqFromPayment : ICodeChunk<GetExpressCheckoutDetailsReq>
    {
        private readonly string _token;

        public GetExpressCheckoutDetailsReqFromPayment(string token)
        {
            _token = token;
        }

        public GetExpressCheckoutDetailsReq Do()
        {
            var request = new GetExpressCheckoutDetailsRequestType
            {
                Version = PayPalConfig.VersionNumber,
                Token = _token
            };

            return new GetExpressCheckoutDetailsReq { GetExpressCheckoutDetailsRequest = request };
        }
    }
}