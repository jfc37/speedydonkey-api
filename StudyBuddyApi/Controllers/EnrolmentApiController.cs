using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class EnrolmentApiController : GenericApiController<UserModel, User>
    {
        public EnrolmentApiController(IActionHandlerOverlord actionHandlerOverlord, IUrlConstructor urlConstructor, IRepository<User> repository, ICommonInterfaceCloner cloner, IEntitySearch<User> entitySearch) : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }

        public HttpResponseMessage Post([FromBody] EnrolmentModel model)
        {
            var user = new UserModel
            {
                Id = model.UserId,
                EnroledBlocks = model.BlockIds.Select(x => (IBlock) new BlockModel{Id = x}).ToList(),
                Passes = model.PassTypes.Select(x => (IPass) new PassModel{PassType = x}).ToList()
            };
            return Post(user, x => new EnrolInBlock(x));
        }
    }
}