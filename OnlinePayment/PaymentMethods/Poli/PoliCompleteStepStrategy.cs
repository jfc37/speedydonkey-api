using System.Linq;
using Data.Repositories;
using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.Poli.Extensions;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli
{
    public class PoliCompleteStepStrategy : ICompletePaymentStrategy<string, PoliCompleteResponse, PoliPayment>
    {
        private readonly IPoliIntergrator _poliIntergrator;
        private readonly IRepository<PoliPayment> _repository;

        public PoliCompleteStepStrategy(
            IPoliIntergrator poliIntergrator,
            IRepository<PoliPayment> repository)
        {
            _poliIntergrator = poliIntergrator;
            _repository = repository;
        }

        public PoliCompleteResponse CompletePayment(string token)
        {
            var result = _poliIntergrator.GetTransaction(token);

            return result.ToPoliCompleteResponse();
        }

        public PoliPayment GetCompletedPayment(string token)
        {
            var onlinePayment = _repository
                .GetAll()
                .Single(x => x.Token == token);

            return onlinePayment;
        }

        public PoliCompleteResponse GetPaymentAlreadyCompleteResponse()
        {
            return new PoliCompleteResponse
            {
                Status = "Already Completed"
            };
        }
    }
}
