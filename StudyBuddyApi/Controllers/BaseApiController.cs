using System.Web.Http;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{

    [BasicAuthAuthorise]
    public abstract class BaseApiController : ApiController
    {
    }
}
