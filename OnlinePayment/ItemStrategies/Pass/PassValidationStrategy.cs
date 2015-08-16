using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using Validation.RuleRunners;
using Validation.Rules;

namespace OnlinePayments.ItemStrategies.Pass
{
    public class PassValidationStrategy : ITypedItemValidationStrategy<PassTemplate>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<PassTemplate> _repository;

        public PassValidationStrategy(ICurrentUser currentUser, IRepository<PassTemplate> repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public bool IsValid(string itemId)
        {
            if (itemId.IsNotInt())
                return false;

            IRule[] rules = GetRules(itemId);
            return new AllRulesAreValidRunner(rules)
                .Run();
        }

        private IRule[] GetRules(string itemId)
        {
            return new IRule[]
            {
                new IsUserLoggedInRule(_currentUser),
                new DoesIdExist<PassTemplate>(_repository, itemId.ToInt())
            };
        }
    }
}
