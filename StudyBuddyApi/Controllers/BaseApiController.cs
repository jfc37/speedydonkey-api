using System.Web.Http;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{

    [Authorize]
    public abstract class BaseApiController : ApiController
    {
    }
}
