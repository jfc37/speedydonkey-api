using Common;

namespace Validation.Rules
{
    public class IsUserLoggedInRule : IRule
    {
        private readonly ICurrentUser _currentUser;

        public IsUserLoggedInRule(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public bool IsValid()
        {
            return _currentUser.IsLoggedIn;
        }
    }
}
