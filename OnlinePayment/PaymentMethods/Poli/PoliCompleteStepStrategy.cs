using System.Linq;
using Common;
using Data.Repositories;
using Models;
using Models.OnlinePayments;
using OnlinePayments.PaymentMethods.PayPal;
using OnlinePayments.PaymentMethods.PayPal.Models;
using OnlinePayments.PaymentMethods.Poli.Models;

namespace OnlinePayments.PaymentMethods.Poli
{
    public class PoliCompleteStepStrategy : IPaymentStepStrategy<string, PoliCompleteResponse>
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

        public PoliCompleteResponse PerformStep(string token)
        {
            var onlinePayment = _repository
                .GetAll()
                .Single(x => x.Token == token);

            var result = _poliIntergrator.GetTransaction(onlinePayment.Token);

            //if (result.Errors.NotAny())
            //{
            //    onlinePayment.PaymentStatus = OnlinePaymentStatus.Complete;
            //    _repository.Update(onlinePayment);
            //}

            return result;
        }
    }
}
