using System.Web.Http;
using Action;
using ActionHandlers;
using Common.Extensions;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/passes")]
    public class PassNoteApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public PassNoteApiController(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        [Route("{id}/notes")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Put(int id, [FromBody] string note)
        {
            var pass = new Pass(id) {Note = note};
            var action = new UpdatePassNote(pass);

            var result = _actionHandlerOverlord.HandleAction<UpdatePassNote, Pass>(action);

            return result.ValidationResult.IsValid 
                ? (IHttpActionResult) Ok() 
                : BadRequest(result.ToJson());
        }
    }
}