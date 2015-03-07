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

    public class CreateEntityHandler<TAction, TEntity> : IActionHandler<TAction, TEntity> where TAction : ICreateAction<TEntity> where TEntity : IEntity
    {
        private readonly IRepository<TEntity> _repository;

        public CreateEntityHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Handle(TAction action)
        {
            PreHandle(action);
            var result = _repository.Create(action.ActionAgainst);
            PostHandle(action, result);
            return result;
        }

        protected virtual void PostHandle(ICreateAction<TEntity> action, IEntity result) { }

        protected virtual void PreHandle(ICreateAction<TEntity> action){ }
    }
}
