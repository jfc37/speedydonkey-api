using Models.OnlinePayments;

namespace OnlinePayments.PaymentFeeStrategies
{
    public interface IPaymentFeeStrategy
    {
        decimal GetFee(OnlinePayment onlinePayment);
    }
}
