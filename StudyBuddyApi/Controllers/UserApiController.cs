using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserApiController : GenericController<UserModel, User>
    {
        public UserApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor,
            IRepository<User> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<User> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch) { }

        public HttpResponseMessage Post([FromBody] UserModel model)
        {
            return Post(model, x => new CreateUser(x));
        }

        public HttpResponseMessage Post(int userId, int blockId)
        {
            return Post(new UserModel
            {
                Id = userId,
                EnroledBlocks = new List<IBlock> { new BlockModel { Id = blockId } }
            }, x => new EnrolInBlock(x));
        }

        public HttpResponseMessage Post(int userId, [FromBody]PassModel pass)
        {
            return Post(new UserModel
            {
                Id = userId,
                Passes = new List<IPass> { pass }
            }, x => new PurchasePass(x));
        }
    }
}
