using System.Web.Http;

namespace SpeedyDonkeyApi.Controllers.Emails
{
    [RoutePrefix("api/emails/verification")]
    public class VerificationApiController : ApiController
    {
        [Route]
        public IHttpActionResult Post(object user)
        {
            return Ok();
        }
    }
}
