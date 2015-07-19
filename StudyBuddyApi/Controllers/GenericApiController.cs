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
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public abstract class GenericApiController<TModel, TEntity> : BaseApiController where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity
    {
        protected readonly IActionHandlerOverlord _actionHandlerOverlord;
        protected readonly IUrlConstructor _urlConstructor;
        protected readonly IRepository<TEntity> _repository;
        protected readonly ICommonInterfaceCloner _cloner;
        protected readonly IEntitySearch<TEntity> _entitySearch;

        protected GenericApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IUrlConstructor urlConstructor,
            IRepository<TEntity> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<TEntity> entitySearch)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            _urlConstructor = urlConstructor;
            _repository = repository;
            _cloner = cloner;
            _entitySearch = entitySearch;
        }

        protected HttpResponseMessage PerformAction<TAction>([FromBody]TModel model, Func<TEntity,TAction> actionCreator) where TAction : IAction<TEntity>
        {
            var entity = model.ToEntity(_cloner);
            var action = actionCreator(entity);
            ActionReponse<TEntity> result = _actionHandlerOverlord.HandleAction<TAction, TEntity>(action);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<IApiModel<TEntity>>
                {
                    ActionResult = model.CloneFromEntity(Request, _urlConstructor, result.ActionResult, _cloner),
                    ValidationResult = result.ValidationResult
                });
        }
        public virtual HttpResponseMessage Get(int id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var model = new TModel().CloneFromEntity(Request, _urlConstructor, entity, _cloner);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
        public virtual HttpResponseMessage Get()
        {
            var allEntities = _repository.GetAll();
            if (!allEntities.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var allModels = allEntities
                .Select(x => new TModel().CloneFromEntity(Request, _urlConstructor, x, _cloner))
                .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, allModels);
        }

        public virtual HttpResponseMessage Get(string q)
        {
            var matchingEntities = _entitySearch.Search(q);
            if (!matchingEntities.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK,
                matchingEntities.Select(x => new TModel().CloneFromEntity(Request, _urlConstructor, x, _cloner)).ToList());
        }
    }
}