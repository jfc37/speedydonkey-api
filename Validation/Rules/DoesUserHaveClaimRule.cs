using Common;
using Common.Extensions;
using Models;

namespace Validation.Rules
{
    public class DoesUserHaveClaimRule : IRule
    {
        private readonly User _user;
        private readonly Claim _claim;

        public DoesUserHaveClaimRule(User user, Claim claim)
        {
            _user = user;
            _claim = claim;
        }

        public bool IsValid()
        {
            return _user.Claims.IsNotNullOrWhiteSpace() && _user.Claims.Contains(_claim.ToString());
        }
    }
}