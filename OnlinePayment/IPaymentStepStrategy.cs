namespace OnlinePayments
{
    public interface IPaymentStepStrategy<in TPayment, out TResponse>
    {
        TResponse PerformStep(TPayment payment);
    }
}