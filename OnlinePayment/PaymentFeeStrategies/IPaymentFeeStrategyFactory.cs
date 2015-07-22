using Models.OnlinePayments;

namespace OnlinePayments.PaymentFeeStrategies
{
    public interface IPaymentFeeStrategyFactory
    {
        IPaymentFeeStrategy GetPaymentFeeStrategy(PaymentMethod paymentMethod);
    }
}