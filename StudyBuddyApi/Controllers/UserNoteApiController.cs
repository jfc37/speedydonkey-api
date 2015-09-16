using System.Web.Http;
using Action;
using ActionHandlers;
using Common.Extensions;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserNoteApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public UserNoteApiController(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        [Route("{id}/notes")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Put(int id, [FromBody] string note)
        {
            var user = new User(id) {Note = note};
            var action = new UpdateUserNote(user);

            var result = _actionHandlerOverlord.HandleAction<UpdateUserNote, User>(action);

            return result.ValidationResult.IsValid 
                ? (IHttpActionResult) Ok() 
                : BadRequest(result.ToJson());
        }
    }
}