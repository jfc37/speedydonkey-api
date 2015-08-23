using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Data.Searches;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public abstract class GenericApiController<TModel, TEntity> : BaseApiController where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity
    {
        protected readonly IActionHandlerOverlord _actionHandlerOverlord;
        protected readonly IRepository<TEntity> _repository;
        protected readonly IEntitySearch<TEntity> _entitySearch;

        protected GenericApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<TEntity> repository,
            IEntitySearch<TEntity> entitySearch)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            _repository = repository;
            _entitySearch = entitySearch;
        }

        protected HttpResponseMessage PerformAction<TAction, TM, TE>([FromBody]TM model, Func<TE,TAction> actionCreator) 
            where TAction : IAction<TE>
            where TM : IApiModel<TE>, new()
            where TE : class, IEntity
        {
            var entity = model.ToEntity();
            var action = actionCreator(entity);
            ActionReponse<TE> result = _actionHandlerOverlord.HandleAction<TAction, TE>(action);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<IApiModel<TE>>
                {
                    ActionResult = model.CloneFromEntity(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        protected HttpResponseMessage PerformAction<TAction>([FromBody]TModel model, Func<TEntity,TAction> actionCreator) where TAction : IAction<TEntity>
        {
            return PerformAction<TAction, TModel, TEntity>(model, actionCreator);
        }
        public virtual HttpResponseMessage Get(int id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var model = new TModel().CloneFromEntity(Request, entity);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
        public virtual HttpResponseMessage Get()
        {
            var allEntities = _repository.GetAll();
            if (!allEntities.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var allModels = allEntities
                .Select(x => new TModel().CloneFromEntity(Request, x))
                .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, allModels);
        }

        public virtual HttpResponseMessage Get(string q)
        {
            var matchingEntities = _entitySearch.Search(q);
            if (!matchingEntities.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK,
                matchingEntities.Select(x => new TModel().CloneFromEntity(Request, x)).ToList());
        }
    }
}