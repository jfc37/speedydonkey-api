using Data.Repositories;
using Models;
using Models.OnlinePayments;
using Validation.Rules;

namespace OnlinePayments.ItemStrategies.WindyLindy
{
    public class WindyLindyValidationStrategy : ITypedItemValidationStrategy<Registration>
    {
        private readonly IRepository<Registration> _repository;

        public WindyLindyValidationStrategy(IRepository<Registration> repository)
        {
            _repository = repository;
        }

        public bool IsValid(string itemId)
        {
            return new IsThereARegistrationWithMatchingRegistrationNumber(_repository, itemId)
                .IsValid();
        }
    }
}