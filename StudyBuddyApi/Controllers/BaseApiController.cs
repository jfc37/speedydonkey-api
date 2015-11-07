using System.Web.Http;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{

    [Authorize]
    [CreateNewUserIfRequired]
    [CurrentUser]
    public abstract class BaseApiController : ApiController
    {
    }
}
