using Data.CodeChunks;
using Models.OnlinePayments;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.CodeChunks
{
    public class GetSetExpressCheckoutReqFromPayment : ICodeChunk<SetExpressCheckoutReq>
    {
        private readonly PayPalPayment _payment;

        public GetSetExpressCheckoutReqFromPayment(PayPalPayment payment)
        {
            _payment = payment;
        }

        public SetExpressCheckoutReq Do()
        {
            var request = new GetSetExpressCheckoutRequestTypeFromPayment(_payment).Do();

            return new SetExpressCheckoutReq { SetExpressCheckoutRequest = request };
        }
    }
}