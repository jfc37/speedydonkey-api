using Models;

namespace Validation.Rules
{
    public class IsUserMissingClaimRule : IRule
    {
        private readonly User _user;
        private readonly Claim _claim;

        public IsUserMissingClaimRule(User user, Claim claim)
        {
            _user = user;
            _claim = claim;
        }
        public bool IsValid()
        {
            return !new DoesUserHaveClaimRule(_user, _claim)
                .IsValid();
        }
    }
}