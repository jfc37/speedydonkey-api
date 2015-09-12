using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/blocks")]
    public class BlockApiController : GenericApiController<Block>
    {
        public BlockApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<Block> repository,
            IEntitySearch<Block> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post(int levelId)
        {
            var blockModel = new BlockModel {Level = new LevelModel {Id = levelId}};
            var result = PerformAction<CreateBlock, Block>(new CreateBlock(blockModel.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<BlockModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post()
        {
            var result = PerformAction<GenerateBlocksForAllLevels, Block>(new GenerateBlocksForAllLevels(new Block()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<BlockModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Put(int id, [FromBody] BlockModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateBlock, Block>(new UpdateBlock(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<BlockModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new Block {Id = id};
            var result = PerformAction<DeleteBlock, Block>(new DeleteBlock(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<BlockModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}
