using System;

namespace Validation.Rules
{
    public class IsStringAValidGuidRule : IRule
    {
        private readonly string _toCheck;

        public IsStringAValidGuidRule(string toCheck)
        {
            _toCheck = toCheck;
        }

        public bool IsValid()
        {
            Guid guid;
            return Guid.TryParse(_toCheck, out guid);
        }
    }
}
