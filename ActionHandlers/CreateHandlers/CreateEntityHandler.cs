using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateEntityHandler<TAction, TEntity> : IActionHandler<TAction, TEntity> where TAction : ICreateAction<TEntity> where TEntity : IEntity
    {
        private readonly IRepository<TEntity> _repository;
        protected bool ShouldCreateEntity;

        public CreateEntityHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
            ShouldCreateEntity = true;
        }

        public TEntity Handle(TAction action)
        {
            PreHandle(action);
            if (ShouldCreateEntity)
            {
                var result = _repository.Create(action.ActionAgainst);
                PostHandle(action, result);
                return result;   
            }
            return action.ActionAgainst;
        }

        protected virtual void PostHandle(ICreateAction<TEntity> action, IEntity result) { }

        protected virtual void PreHandle(ICreateAction<TEntity> action){ }
    }
}