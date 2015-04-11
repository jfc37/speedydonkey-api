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
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class CurrentUserApiController : BaseApiController
    {
        private readonly IRepository<User> _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public CurrentUserApiController(
            IRepository<User> repository, 
            ICurrentUser currentUser,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
        {
            _repository = repository;
            _currentUser = currentUser;
            _urlConstructor = urlConstructor;
            _cloner = cloner;
            _actionHandlerOverlord = actionHandlerOverlord;
        }
        
        [ActiveUserRequired]
        public HttpResponseMessage Put([FromBody]UserModel model)
        {
            model.Id = _currentUser.Id;
            var user = model.ToEntity(_cloner);
            var updateUser = new UpdateUser(user);
            ActionReponse<User> result = _actionHandlerOverlord.HandleAction<UpdateUser, User>(updateUser);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<IApiModel<User>>
                {
                    ActionResult = model.CloneFromEntity(Request, _urlConstructor, result.ActionResult, _cloner),
                    ValidationResult = result.ValidationResult
                });
        }

        [ActiveUserRequired]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(new UserModel().CloneFromEntity(Request, _urlConstructor, _repository.Get(_currentUser.Id), _cloner));
        }
    }
}