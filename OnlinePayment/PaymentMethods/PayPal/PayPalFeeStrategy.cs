using Models.OnlinePayments;
using OnlinePayments.PaymentFeeStrategies;

namespace OnlinePayments.PaymentMethods.PayPal
{
    public class PayPalFeeStrategy : IPaymentFeeStrategy
    {
        public decimal GetFee(OnlinePayment onlinePayment)
        {
            return new PayPalFeeCalculation(onlinePayment.Price)
                .Calculate()
                .Result();
        }
    }
}