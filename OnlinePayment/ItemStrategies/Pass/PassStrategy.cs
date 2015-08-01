using Common.Extensions;
using Data.Repositories;
using Models;

namespace OnlinePayments.ItemStrategies.Pass
{
    public class PassStrategy : ITypedItemStrategy<PassTemplate>
    {
        private readonly IRepository<PassTemplate> _repository;

        public PassStrategy(IRepository<PassTemplate> repository)
        {
            _repository = repository;
        }

        public decimal GetPrice(string itemId)
        {
            return _repository.Get(itemId.ToInt()).Cost;
        }

        public string GetDescription(string itemId)
        {
            return _repository.Get(itemId.ToInt()).Description;
        }
    }
}
