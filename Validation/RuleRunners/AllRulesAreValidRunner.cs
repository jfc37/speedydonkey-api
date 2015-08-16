using System.Linq;
using Validation.Rules;

namespace Validation.RuleRunners
{
    public class AllRulesAreValidRunner : IRuleRunner
    {
        private readonly IRule[] _rules;

        public AllRulesAreValidRunner(params IRule[] rules)
        {
            _rules = rules;
        }

        public bool Run()
        {
            return _rules.All(x => x.IsValid());
        }
    }
}