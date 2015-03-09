using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateUserHandler : CreateEntityHandler<CreateUser, User>
    {
        private readonly IRepository<Account> _accountRepository;

        public CreateUserHandler(
            IRepository<User> repository,
            IRepository<Account> accountRepository) : base(repository)
        {
            _accountRepository = accountRepository;
        }

        protected override void PreHandle(ICreateAction<User> action)
        {
            action.ActionAgainst.Account = _accountRepository.Get(action.ActionAgainst.Account.Id);
        }

        protected override void PostHandle(ICreateAction<User> action, IEntity result)
        {
            action.ActionAgainst.Account.User = action.ActionAgainst;
            _accountRepository.Update((Account)action.ActionAgainst.Account);
        }
    }
}
