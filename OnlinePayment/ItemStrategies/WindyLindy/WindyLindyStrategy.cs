using System.Linq;
using Common.Extensions;
using Data.CodeChunks;
using Data.Repositories;
using Models;
using Models.OnlinePayments;

namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyStrategy : ITypedItemStrategy<Registration>
    {
        private readonly IRepository<Registration> _repository;

        public WindyLindyStrategy(IRepository<Registration> repository)
        {
            _repository = repository;
        }

        public decimal GetPrice(string itemId)
        {
            return new decimal(199.99);
        }

        public string GetDescription(string itemId)
        {
            return "Full Pass";
        }

        public void CompletePurchase(OnlinePayment completedPayment)
        {
            var registration = new GetRegistrationFromRegistrationNumber(_repository, completedPayment.ItemId.ToGuid())
                    .Do();

            registration.PaymentStatus = OnlinePaymentStatus.Complete;
            _repository.Update(registration);
        }
    }
}