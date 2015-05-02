using ActionHandlers.CreateHandlers;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.DeleteHandlers
{
    public class DeleteEntityHandler<TAction, TEntity> : CrudEntityHandler<TAction, TEntity> where TAction : ICrudAction<TEntity> where TEntity : IEntity
    {
        private readonly IRepository<TEntity> _repository;

        public DeleteEntityHandler(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        protected override TEntity PerformAction(TAction action)
        {
            _repository.Delete(action.ActionAgainst.Id);
            return action.ActionAgainst;
        }
    }
}