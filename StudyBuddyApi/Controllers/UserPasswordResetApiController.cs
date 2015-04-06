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
    public class UserPasswordResetApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public UserPasswordResetApiController(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        [AllowAnonymous]
        public HttpResponseMessage Post([FromBody]string email)
        {
            var user = new User
            {
                Email = email
            };
            var forgottenPassword = new ForgottenPassword(user);
            _actionHandlerOverlord.HandleAction<ForgottenPassword, User>(forgottenPassword);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        public HttpResponseMessage Put([FromBody]Guid key, [FromBody]string password)
        {
            var user = new User
            {
                ActivationKey = key,
                Password = password
            };
            var resetPassword = new ResetPassword(user);
            var response = _actionHandlerOverlord.HandleAction<ResetPassword, User>(resetPassword);
            return response.ValidationResult.IsValid ?
                Request.CreateResponse(HttpStatusCode.OK) :
                Request.CreateResponse(HttpStatusCode.BadRequest, response.ValidationResult);
        }
    }
}