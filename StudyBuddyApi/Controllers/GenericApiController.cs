using System.Collections.Generic;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Data.Searches;

namespace SpeedyDonkeyApi.Controllers
{
    public abstract class GenericApiController<TEntity> : BaseApiController where TEntity : class, IEntity
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        protected readonly IRepository<TEntity> Repository;
        private readonly IEntitySearch<TEntity> _entitySearch;

        protected GenericApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<TEntity> repository,
            IEntitySearch<TEntity> entitySearch)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            Repository = repository;
            _entitySearch = entitySearch;
        }

        protected ActionReponse<TE> PerformAction<TAction, TE>(TAction action)
            where TAction : SystemAction<TE>
            where TE : class, IEntity
        {
            ActionReponse<TE> result = _actionHandlerOverlord.HandleAction<TAction, TE>(action);
            return result;
        }
        protected TEntity GetById(int id)
        {
            var entity = Repository.Get(id);
            return entity;
        }
        protected IEnumerable<TEntity> GetAll()
        {
            var allEntities = Repository.Queryable();
            return allEntities;
        }

        protected IEnumerable<TEntity> Search(string q)
        {
            var matchingEntities = _entitySearch.Search(q);
            return matchingEntities;
        }
    }
}