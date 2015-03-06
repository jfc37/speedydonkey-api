using Models;

namespace Actions
{
    public class CreateAccount : IAction<Account>
    {
        public CreateAccount(Account account)
        {
            ActionAgainst = account;
        }

        public Account ActionAgainst { get; set; }
    }
}
