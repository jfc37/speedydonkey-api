using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class BlockApiController : GenericApiController<BlockModel, Block>
    {
        public BlockApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<Block> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<Block> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post(int levelId)
        {
            return PerformAction(new BlockModel {Level = new LevelModel {Id = levelId}}, x => new CreateBlock(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post()
        {
            return PerformAction(new BlockModel(), x => new GenerateBlocksForAllLevels(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody] BlockModel model)
        {
            model.Id = id;
            return PerformAction(model, x => new UpdateBlock(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new BlockModel {Id = id};
            return PerformAction(model, x => new DeleteBlock(x));
        }
    }
}
