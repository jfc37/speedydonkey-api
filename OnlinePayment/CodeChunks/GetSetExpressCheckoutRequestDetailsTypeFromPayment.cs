using Data.CodeChunks;
using Models.OnlinePayments;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.CodeChunks
{
    public class GetSetExpressCheckoutRequestDetailsTypeFromPayment : ICodeChunk<SetExpressCheckoutRequestDetailsType>
    {
        private readonly PayPalPayment _payment;

        public GetSetExpressCheckoutRequestDetailsTypeFromPayment(PayPalPayment payment)
        {
            _payment = payment;
        }

        public SetExpressCheckoutRequestDetailsType Do()
        {
            var paymentDetails = new GetPayPalPaymentDetailsFromPayment(_payment).Do();

            var ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = _payment.ReturnUrl;
            ecDetails.CancelURL = _payment.CancelUrl;
            ecDetails.NoShipping = "1";
            ecDetails.ReqConfirmShipping = "0";
            ecDetails.SolutionType = SolutionTypeType.SOLE;
            ecDetails.LandingPage = LandingPageType.BILLING;

            ecDetails.PaymentDetails = paymentDetails;

            return ecDetails;
        }
    }
}