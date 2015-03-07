using Models;

namespace Actions
{
    public class CreateAccount : ICreateAction<Account>
    {
        public CreateAccount(Account account)
        {
            ActionAgainst = account;
        }

        public Account ActionAgainst { get; set; }
    }
}
