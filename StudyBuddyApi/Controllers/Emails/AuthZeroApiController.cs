using System.Web.Http;
using AuthZero.Interfaces;
using Contracts.Users;

namespace SpeedyDonkeyApi.Controllers.Emails
{
    [RoutePrefix("api/emails/verification")]
    public class AuthZeroApiController : ApiController
    {
        private readonly IAuthZeroEmailService _authZeroEmailService;

        public AuthZeroApiController(IAuthZeroEmailService authZeroEmailService)
        {
            _authZeroEmailService = authZeroEmailService;
        }

        [Route]
        //[Authorize]
        public IHttpActionResult Verification(AuthZeroUserModel model)
        {
            _authZeroEmailService.SendVerification(model.UserId);

            return Ok();
        }
    }
}
