using Models.OnlinePayments;

namespace OnlinePayments.PaymentFeeStrategies
{
    public class NoFeeStrategy : IPaymentFeeStrategy
    {
        public decimal GetFee(OnlinePayment onlinePayment)
        {
            return 0;
        }
    }
}