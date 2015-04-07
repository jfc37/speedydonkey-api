using System.Web.Http;
using System.Web.Http.Cors;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{

    [BasicAuthAuthorise]
    public abstract class BaseApiController : ApiController
    {
    }
}
