using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateAccountHandler : CreateEntityHandler<CreateAccount, Account>
    {
        private readonly IPasswordHasher _passwordHasher;

        public CreateAccountHandler(
            IRepository<Account> repository, 
            IPasswordHasher passwordHasher) : base(repository)
        {
            _passwordHasher = passwordHasher;
        }

        protected override void PreHandle(ICreateAction<Account> action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
        }
    }
}
