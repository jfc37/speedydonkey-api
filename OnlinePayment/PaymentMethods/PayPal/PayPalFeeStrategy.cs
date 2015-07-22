using Models.OnlinePayments;

namespace OnlinePayments.PaymentFeeStrategies
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