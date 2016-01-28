using ActionHandlers.CreateHandlers;
using Actions;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.DeleteHandlers
{
    public class DeleteEntityHandler<TAction, TEntity> : CrudEntityHandler<TAction, TEntity> 
        where TAction : SystemAction<TEntity>, ICrudAction<TEntity> where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public DeleteEntityHandler(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected override TEntity PerformAction(TAction action)
        {
            Repository.Delete(action.ActionAgainst.Id);
            return action.ActionAgainst;
        }
    }
}