using Data.CodeChunks;
using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.CodeChunks
{
    public class GetSetExpressCheckoutRequestTypeFromPayment : ICodeChunk<SetExpressCheckoutRequestType>
    {
        private readonly PayPalPayment _payment;
        private const string PayPalVersionNumber = "104.0";

        public GetSetExpressCheckoutRequestTypeFromPayment(PayPalPayment payment)
        {
            _payment = payment;
        }

        public SetExpressCheckoutRequestType Do()
        {
            var ecDetails = new GetSetExpressCheckoutRequestDetailsTypeFromPayment(_payment).Do();

            var request = new SetExpressCheckoutRequestType();
            request.Version = PayPalVersionNumber;
            request.SetExpressCheckoutRequestDetails = ecDetails;
            return request;
        }
    }
}