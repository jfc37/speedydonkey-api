using Models;

namespace Actions
{
    public interface ICreateAction<TEntity> : IAction<TEntity> { }

    public class CreateAccount : ICreateAction<Account>
    {
        public CreateAccount(Account account)
        {
            ActionAgainst = account;
        }

        public Account ActionAgainst { get; set; }
    }
}
