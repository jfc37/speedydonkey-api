using System;
using Data.CodeChunks;
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
            payment.SuccessUrl = new ReferenceNumberPlaceholderReplacement(payment.SuccessUrl, payment.ReferenceNumber).Do();
            payment.CancellationUrl = new ReferenceNumberPlaceholderReplacement(payment.CancellationUrl, payment.ReferenceNumber).Do();
            payment.FailureUrl = new ReferenceNumberPlaceholderReplacement(payment.FailureUrl, payment.ReferenceNumber).Do();

            var response = _poliIntergrator.InitiateTransaction(payment);

            payment.PoliId = response.PoliId;
            payment.Token = response.Token;

            response.ReferenceNumber = payment.ReferenceNumber;
            return response;
        }
    }
}