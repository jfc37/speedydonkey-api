using Actions;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public abstract class CrudEntityHandler<TAction, TEntity> : IActionHandler<TAction, TEntity>
        where TAction : ICrudAction<TEntity>
        where TEntity : IEntity
    {
        public TEntity Handle(TAction action)
        {
            PreHandle(action);
            var result = PerformAction(action);
            PostHandle(action, result);
            return result;
        }

        protected abstract TEntity PerformAction(TAction action);

        protected virtual void PostHandle(ICrudAction<TEntity> action, TEntity result) { }

        protected virtual void PreHandle(ICrudAction<TEntity> action) { }
    }

    public class CreateEntityHandler<TAction, TEntity> : CrudEntityHandler<TAction, TEntity> where TAction : ICrudAction<TEntity> where TEntity : IEntity
    {
        private readonly IRepository<TEntity> _repository;

        public CreateEntityHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        protected override TEntity PerformAction(TAction action)
        {
            return _repository.Create(action.ActionAgainst);
        }
    }
}