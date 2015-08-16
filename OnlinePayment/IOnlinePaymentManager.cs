using Models.OnlinePayments;

namespace OnlinePayments
{
    public interface IOnlinePaymentManager
    {
        TResponse Begin<TPayment, TResponse>(
            TPayment payment, 
            IStartPaymentStrategy<TPayment, TResponse> paymentStrategy,
            IResponseCreator<TResponse> responseCreator)
            where TPayment : OnlinePayment
            where TResponse : IStartOnlinePaymentResponse;

        TResponse Complete<TPaymentId, TPayment, TResponse>(
            TPaymentId payment,
            ICompletePaymentStrategy<TPaymentId, TResponse, TPayment> paymentStrategy)
            where TResponse : ICompleteOnlinePaymentResponse
            where TPayment : OnlinePayment;
    }
}