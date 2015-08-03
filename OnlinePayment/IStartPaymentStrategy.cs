using Models.OnlinePayments;

namespace OnlinePayments
{
    public interface IStartPaymentStrategy<in TPayment, out TResponse>
        where TPayment : OnlinePayment
        where TResponse : IStartOnlinePaymentResponse
    {
        TResponse StartPayment(TPayment payment);
    }

    public interface ICompletePaymentStrategy<in TPaymentId, out TResponse, TPayment>
        where TResponse : ICompleteOnlinePaymentResponse
    {
        TResponse CompletePayment(TPaymentId payment);
        TPayment GetCompletedPayment(TPaymentId payment);
    }
}