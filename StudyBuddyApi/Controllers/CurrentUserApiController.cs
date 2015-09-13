using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
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
        public IHttpActionResult Put([FromBody]UserModel model)
        {
            model.Id = _currentUser.Id;
            var user = model.ToEntity();
            var updateUser = new UpdateUser(user);
            var result = _actionHandlerOverlord.HandleAction<UpdateUser, User>(updateUser);

            var resultModel = new ActionReponse<UserModel>
                {
                    ActionResult = result.ActionResult.ToModel(),
                    ValidationResult = result.ValidationResult
                };
            return resultModel.ValidationResult.IsValid 
                ? this.BadRequestWithContent(resultModel) 
                : Ok(resultModel);
        }

        [Route]
        [ActiveUserRequired]
        public IHttpActionResult Get()
        {
            return Ok(_repository.Get(_currentUser.Id).ToModel());
        }
    }

    public static class ApiControllerExtensions
    {
        public static IHttpActionResult BadRequestWithContent<T>(this ApiController instance, T content)
        {
            return new NegotiatedContentResult<T>(HttpStatusCode.BadRequest, content, instance);
        }

        public static IHttpActionResult CreatedWithContent<T>(this ApiController instance, T content)
        {
            return new NegotiatedContentResult<T>(HttpStatusCode.Created, content, instance);
        }
    }
}