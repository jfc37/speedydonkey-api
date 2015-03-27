using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
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

        protected virtual void PostHandle(ICreateAction<TEntity> action, TEntity result) { }

        protected virtual void PreHandle(ICreateAction<TEntity> action){ }
    }
}