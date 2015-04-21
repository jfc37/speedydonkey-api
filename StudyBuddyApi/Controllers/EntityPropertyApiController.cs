using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public abstract class EntityPropertyApiController<TViewModel, TModel, TEntity> : BaseApiController
        where TViewModel : IEntityView<TEntity, TModel>, new()
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _entityRepository;
        private readonly IUrlConstructor _urlConstructor;
        protected readonly ICommonInterfaceCloner Cloner;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        protected EntityPropertyApiController(
            IRepository<TEntity> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
        {
            _entityRepository = entityRepository;
            _urlConstructor = urlConstructor;
            Cloner = cloner;
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        public virtual HttpResponseMessage Get(int id)
        {
            var entity = _entityRepository.Get(id);
            if (entity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var userScheduleModels = new TViewModel().ConvertFromEntity(entity, Request, _urlConstructor, Cloner);
            return userScheduleModels.Any()
                ? Request.CreateResponse(userScheduleModels)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        protected HttpResponseMessage PerformAction<TAction, TActionModel, TActionEntity>([FromBody]TActionModel model, Func<TActionEntity, TAction> actionCreator)
            where TAction : IAction<TActionEntity>
            where TActionModel : IApiModel<TActionEntity>, new()
            where TActionEntity : class, IEntity
        {
            var entity = model.ToEntity(Cloner);
            return PerformAction<TAction, TActionModel, TActionEntity>(model, actionCreator(entity));
        }

        protected HttpResponseMessage PerformAction<TAction, TActionModel, TActionEntity>([FromBody]TActionModel model, TAction action)
            where TAction : IAction<TActionEntity>
            where TActionModel : IApiModel<TActionEntity>, new()
            where TActionEntity : class, IEntity
        {
            ActionReponse<TActionEntity> result = _actionHandlerOverlord.HandleAction<TAction, TActionEntity>(action);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<IApiModel<TActionEntity>>
                {
                    ActionResult = model.CloneFromEntity(Request, _urlConstructor, result.ActionResult, Cloner),
                    ValidationResult = result.ValidationResult
                });
        }
    }
}