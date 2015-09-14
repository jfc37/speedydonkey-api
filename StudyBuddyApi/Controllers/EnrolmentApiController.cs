using System.Linq;
using System.Web.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/users")]
    public class EnrolmentApiController : GenericApiController<User>
    {
        public EnrolmentApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<User> repository, 
            IEntitySearch<User> entitySearch) 
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route("{id:int}/enrolment")]
        public IHttpActionResult Post(int id, [FromBody] EnrolmentModel model)
        {
            var user = new UserModel(id) {EnroledBlocks = model.BlockIds.Select(x => new BlockModel(x)).ToList()};

            var result = PerformAction<EnrolInBlock, User>(new EnrolInBlock(user.ToEntity()));

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}