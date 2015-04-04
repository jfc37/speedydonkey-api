using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Action;
using ActionHandlers;
using Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserActivationApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public UserActivationApiController(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
        }

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