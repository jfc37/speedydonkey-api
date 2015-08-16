using System.Collections.Generic;
using System.Linq;
using Common.Extensions;

namespace Validation.Rules
{
    public class HasExactlyOneInSetRule : IRule
    {
        private readonly IEnumerable<object> _set;

        public HasExactlyOneInSetRule(IEnumerable<object> set)
        {
            _set = set;
        }

        public bool IsValid()
        {
            return _set.IsNotNull() && _set.Count() == 1;
        }
    }
}