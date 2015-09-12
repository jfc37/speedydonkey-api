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
                new ActionReponse<UserModel>
                {
                    ActionResult = result.ActionResult.ToModel(),
                    ValidationResult = result.ValidationResult
                });
        }

        [Route]
        [ActiveUserRequired]
        public IHttpActionResult Get()
        {
            return Ok(_repository.Get(_currentUser.Id).ToModel());
        }
    }
}