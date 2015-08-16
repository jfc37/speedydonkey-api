using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli
{
    public interface IPoliIntergrator
    {
        StartPoliPaymentResponse InitiateTransaction(PoliPayment payment);

        GetTransactionResponse GetTransaction(string token);
    }
}