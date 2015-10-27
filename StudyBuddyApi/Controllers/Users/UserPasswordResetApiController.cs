using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers.Users
{
    [RoutePrefix("api/users/password/reset")]
    public class UserPasswordResetApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public UserPasswordResetApiController(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        [Route]
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

        [Route("{id}")]
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