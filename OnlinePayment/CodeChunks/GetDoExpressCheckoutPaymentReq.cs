using Common.Extensions;
using Data.CodeChunks;
using OnlinePayments.PaymentMethods.PayPal;
using OnlinePayments.PaymentMethods.PayPal.Models;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.CodeChunks
{
    public class GetDoExpressCheckoutPaymentReq : ICodeChunk<DoExpressCheckoutPaymentReq>
    {
        private readonly PayPalCompleteRequest _details;

        public GetDoExpressCheckoutPaymentReq(PayPalCompleteRequest details)
        {
            _details = details;
        }

        public DoExpressCheckoutPaymentReq Do()
        {
            var paymentDetail = new PaymentDetailsType();
            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, _details.Amount.ToCurrencyString());

            var request = new DoExpressCheckoutPaymentRequestType { Version = PayPalConfig.VersionNumber };
            var requestDetails = new DoExpressCheckoutPaymentRequestDetailsType
            {
                PaymentDetails = paymentDetail.PutIntoList(),
                Token = _details.Token,
                PayerID = _details.PayerId
            };
            request.DoExpressCheckoutPaymentRequestDetails = requestDetails;

            return new DoExpressCheckoutPaymentReq { DoExpressCheckoutPaymentRequest = request };
        }
    }
}