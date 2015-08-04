using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli
{
    public class PoliPaymentStrategy : IStartPaymentStrategy<PoliPayment, StartPoliPaymentResponse>
    {
        private readonly IPoliIntergrator _poliIntergrator;

        public PoliPaymentStrategy(IPoliIntergrator poliIntergrator)
        {
            _poliIntergrator = poliIntergrator;
        }

        public StartPoliPaymentResponse StartPayment(PoliPayment payment)
        {
            var response = _poliIntergrator.InitiateTransaction(payment);

            payment.PoliId = response.PoliId;
            payment.Token = response.Token;
            return response;
        }
    }
}