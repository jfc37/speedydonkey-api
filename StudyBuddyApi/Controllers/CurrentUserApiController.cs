using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/users/current")]
    public class CurrentUserApiController : BaseApiController
    {
        private readonly IRepository<User> _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public CurrentUserApiController(
            IRepository<User> repository, 
            ICurrentUser currentUser,
            IActionHandlerOverlord actionHandlerOverlord)
        {
            _repository = repository;
            _currentUser = currentUser;
            _actionHandlerOverlord = actionHandlerOverlord;
        }
        
        [Route]
        [ActiveUserRequired]
        public HttpResponseMessage Put([FromBody]UserModel model)
        {
            model.Id = _currentUser.Id;
            var user = model.ToEntity();
            var updateUser = new UpdateUser(user);
            ActionReponse<User> result = _actionHandlerOverlord.HandleAction<UpdateUser, User>(updateUser);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<IApiModel<User>>
                {
                    ActionResult = model.CloneFromEntity(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        [Route]
        [ActiveUserRequired]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(new UserModel().CloneFromEntity(Request, _repository.Get(_currentUser.Id)));
        }
    }
}