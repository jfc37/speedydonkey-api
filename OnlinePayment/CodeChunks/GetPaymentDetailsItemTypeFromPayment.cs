using Common.Extensions;
using Data.CodeChunks;
using Models.OnlinePayments;
using PayPal.PayPalAPIInterfaceService.Model;

namespace OnlinePayments.CodeChunks
{
    public class GetPaymentDetailsItemTypeFromPayment : ICodeChunk<PaymentDetailsItemType>
    {
        private readonly PayPalPayment _payment;

        public GetPaymentDetailsItemTypeFromPayment(PayPalPayment payment)
        {
            _payment = payment;
        }

        public PaymentDetailsItemType Do()
        {
            var paymentItem = new PaymentDetailsItemType();
            paymentItem.Name = _payment.Description;
            paymentItem.Amount = new BasicAmountType(CurrencyCodeType.NZD, _payment.Total.ToCurrencyString());
            paymentItem.Quantity = 1;

            return paymentItem;
        }
    }
}