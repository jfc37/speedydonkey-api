using System.Web.Http;
using SpeedyDonkeyApi.Filter;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace SpeedyDonkeyApi.Controllers
{
    [Authorize]
    [CurrentUser]
    [Log(AttributeTargetElements = MulticastTargets.Method, AttributeTargetMemberAttributes = MulticastAttributes.Public)]
    public abstract class BaseApiController : ApiController
    {
    }
}
