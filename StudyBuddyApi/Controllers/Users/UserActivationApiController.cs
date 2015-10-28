using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Models;

namespace SpeedyDonkeyApi.Controllers.Users
{
    [RoutePrefix("api/users/activation")]
    public class UserActivationApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public UserActivationApiController(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        [Route("{id}")]
        [AllowAnonymous]
        public HttpResponseMessage Post(Guid id)
        {
            var user = new User
            {
                ActivationKey = id
            };
            var activiateUser = new ActivateUser(user);
            var response = _actionHandlerOverlord.HandleAction<ActivateUser, User>(activiateUser);
            return response.ValidationResult.IsValid
                ? Request.CreateResponse(HttpStatusCode.OK)
                : Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}