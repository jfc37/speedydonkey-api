using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class AccountApiController : GenericController<AccountModel, Account>
    {
        public AccountApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IUrlConstructor urlConstructor,
            IRepository<Account> repository) : base(actionHandlerOverlord, urlConstructor, repository) { }

        public HttpResponseMessage Post([FromBody] AccountModel model)
        {
            return Post(model, x => new CreateAccount((Account)x));
        }
    }

    public abstract class GenericController<TModel, TEntity> : BaseApiController where TModel : IApiModel<TEntity>, new() where TEntity : class, IEntity
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IUrlConstructor _urlConstructor;
        private readonly IRepository<TEntity> _repository;

        protected GenericController(
            IActionHandlerOverlord actionHandlerOverlord,
            IUrlConstructor urlConstructor,
            IRepository<TEntity> repository)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            _urlConstructor = urlConstructor;
            _repository = repository;
        }

        protected HttpResponseMessage Post<TAction>([FromBody]TModel model, Func<IEntity,TAction> actionCreator) where TAction : IAction<TEntity>
        {
            var entity = model.ToEntity();
            var action = actionCreator(entity);
            ActionReponse<TEntity> result = _actionHandlerOverlord.HandleAction<TAction, TEntity>(action);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<IApiModel<TEntity>>
                {
                    ActionResult = model.CloneFromEntity(Request, _urlConstructor, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        public HttpResponseMessage Get(int id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var model = new TModel().CloneFromEntity(Request, _urlConstructor, entity);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
        public HttpResponseMessage Get()
        {
            var allEntities = _repository.GetAll()
                .Select(x => new TModel().CloneFromEntity(Request, _urlConstructor, x))
                .ToList();

            return !allEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.NotFound)
                : Request.CreateResponse(HttpStatusCode.OK, allEntities);
        }
    }
}
