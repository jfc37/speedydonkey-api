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
            return GetRegistration(itemId)
                .Amount;
        }

        public string GetDescription(string itemId)
        {
            var registration = GetRegistration(itemId);
            return registration.FullPass.GetValueOrDefault()
                ? "Full Windy Lindy Pass" 
                : "Partial Windy Lindy Pass";
        }

        public void CompletePurchase(OnlinePayment completedPayment)
        {
            var registration = GetRegistration(completedPayment.ItemId);

            registration.PaymentStatus = OnlinePaymentStatus.Complete;
            _repository.Update(registration);
        }

        private Registration GetRegistration(string itemId)
        {
            return new GetRegistrationFromRegistrationNumber(_repository, itemId.ToGuid())
                .Do();
        }
    }
}