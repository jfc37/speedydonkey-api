using System.Web.Http;
using System.Web.Http.Cors;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{

    [BasicAuthAuthorise]
    [EnableCors(origins: "https://spa-speedydonkey.azurewebsites.net,http://localhost:7300", headers: "*", methods: "*")]
    public abstract class BaseApiController : ApiController
    {
    }
}
