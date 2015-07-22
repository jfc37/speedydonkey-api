using Models.OnlinePayments;

namespace OnlinePayments.PaymentStrategies
{
    public interface IPaymentStrategy<in T, TResponse> where T : OnlinePayment
    {
        TResponse Begin(T onlinePaymentRequest);
    }
}