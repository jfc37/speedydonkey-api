using System.Web.Http;
using Action.Users;
using ActionHandlers;
using Common;
using Contracts;
using Contracts.Users;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Users
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
        public IHttpActionResult Put([FromBody]UserModel model)
        {
            model.Id = _currentUser.Id;
            var user = model.ToEntity();
            var updateUser = new UpdateUser(user);
            var result = _actionHandlerOverlord.HandleAction<UpdateUser, User>(updateUser);

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route]
        public IHttpActionResult Get()
        {
            return Ok(_repository.Get(_currentUser.Id).ToModel());
        }

        
        [HttpGet]
        [Route("check")]
        [AllowAnonymous]
        [BasicAuthAuthorise]
        public IHttpActionResult BasicAuthCheck()
        {
            return Ok(_repository.Get(_currentUser.Id).ToModel());
        }
    }
}