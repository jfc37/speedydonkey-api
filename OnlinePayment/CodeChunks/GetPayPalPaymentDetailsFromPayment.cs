using System.Collections.Generic;
using Common.Extensions;
using Data.CodeChunks;
using Models.OnlinePayments;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.CodeChunks
{
    /// <summary>
    /// Given a PayPalPayment, converts it into 
    /// </summary>
    public class GetPayPalPaymentDetailsFromPayment : ICodeChunk<List<PaymentDetailsType>>
    {
        private readonly PayPalPayment _payment;

        public GetPayPalPaymentDetailsFromPayment(PayPalPayment payment)
        {
            _payment = payment;
        }

        public List<PaymentDetailsType> Do()
        {
            var paymentItems = new GetPaymentDetailsItemTypeFromPayment(_payment)
                .Do()
                .PutIntoList();

            var paymentDetail = new PaymentDetailsType();
            paymentDetail.PaymentDetailsItem = paymentItems;
            paymentDetail.PaymentAction = PaymentActionCodeType.SALE;
            paymentDetail.OrderTotal = new BasicAmountType(CurrencyCodeType.NZD, _payment.Price.ToCurrencyString());

            return paymentDetail.PutIntoList();
        }
    }
}