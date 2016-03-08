using System.Web.Http;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{

    [Authorize]
    [CurrentUser]
    public abstract class BaseApiController : ApiController
    {
    }
}
