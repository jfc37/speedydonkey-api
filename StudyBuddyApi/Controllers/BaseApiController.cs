using System.Web.Http;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{

    [Authorize]
    [CreateNewUserIfRequired]
    public abstract class BaseApiController : ApiController
    {
    }
}
