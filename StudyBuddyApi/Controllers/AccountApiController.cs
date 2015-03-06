using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class AccountApiController : GenericController<AccountModel, Account>
    {
        public AccountApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IUrlConstructor urlConstructor) : base(actionHandlerOverlord, urlConstructor)
        {
        }

        public HttpResponseMessage Post([FromBody] AccountModel model)
        {
            return Post(model, x => new CreateAccount((Account)x));
        }
    }

    public abstract class GenericController<TModel, TEntity> : BaseApiController where TModel : IApiModel where TEntity : IEntity
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IUrlConstructor _urlConstructor;

        protected GenericController(
            IActionHandlerOverlord actionHandlerOverlord,
            IUrlConstructor urlConstructor)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            _urlConstructor = urlConstructor;
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
                new ActionReponse<IApiModel>
                {
                    ActionResult = model.CloneFromEntity(Request, _urlConstructor, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }
    }
}
