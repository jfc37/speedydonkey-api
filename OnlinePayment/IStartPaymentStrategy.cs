using Models.OnlinePayments;

namespace OnlinePayments
{
    public interface IStartPaymentStrategy<in TPayment, out TResponse>
        where TPayment : OnlinePayment
        where TResponse : IStartOnlinePaymentResponse
    {
        TResponse StartPayment(TPayment payment);
    }
}