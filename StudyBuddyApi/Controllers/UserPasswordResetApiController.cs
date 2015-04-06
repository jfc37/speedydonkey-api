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
using SpeedyDonkeyApi.Models;

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
        public HttpResponseMessage Post([FromBody]UserModel userModel)
        {
            var user = new User
            {
                Email = userModel.Email
            };
            var forgottenPassword = new ForgottenPassword(user);
            _actionHandlerOverlord.HandleAction<ForgottenPassword, User>(forgottenPassword);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        public HttpResponseMessage Put([FromBody]UserModel userModel, Guid id)
        {
            var user = new User
            {
                ActivationKey = id,
                Password = userModel.Password
            };
            var resetPassword = new ResetPassword(user);
            var response = _actionHandlerOverlord.HandleAction<ResetPassword, User>(resetPassword);
            return response.ValidationResult.IsValid ?
                Request.CreateResponse(HttpStatusCode.OK) :
                Request.CreateResponse(HttpStatusCode.BadRequest, response.ValidationResult);
        }
    }
}