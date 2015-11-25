using System.Web.Http;
using Action;
using Action.Blocks;
using ActionHandlers;
using Data.QueryFilters;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers.Blocks
{
    [RoutePrefix("api/blocks")]
    public class BlockApiController : GenericApiController<Block>
    {
        public BlockApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<Block> repository,
            IEntitySearch<Block> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route("for-enrolment")]
        public IHttpActionResult GetForEnrolment()
        {
            return new SetToHttpActionResult<Block>(this, new AvailableBlocksForEnrolmentFilter().Filter(GetAll()), x => x.ToStripedModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<Block>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<Block>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<Block>(this, GetById(id), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post(int id)
        {
            var blockModel = new BlockModel(id);
            var result = PerformAction<CreateNextBlock, Block>(new CreateNextBlock(blockModel.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<Block, BlockModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        [NullModelActionFilter]
        public IHttpActionResult Post([FromBody] BlockModel blockModel)
        {
            var result = PerformAction<CreateBlock, Block>(new CreateBlock(blockModel.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<Block, BlockModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Put(int id, [FromBody] BlockModel model)
        {
            model.Id = id;
            var result = PerformAction<UpdateBlock, Block>(new UpdateBlock(model.ToEntity()));

            return new ActionResultToOkHttpActionResult<Block, BlockModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Delete(int id)
        {
            var model = new Block {Id = id};
            var result = PerformAction<DeleteBlock, Block>(new DeleteBlock(model));

            return new ActionResultToOkHttpActionResult<Block, BlockModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}/rooms/{roomId:int}")]
        [NullModelActionFilter]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Put(int id, int roomId)
        {
            var block = new Block(id)
            {
                Room = new Room(roomId)
            };

            var result = PerformAction<ChangeBlockRoom, Block>(new ChangeBlockRoom(block));

            return new ActionResultToOkHttpActionResult<Block, BlockModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}/rooms")]
        [NullModelActionFilter]
        [HttpDelete]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult UnassignRoom(int id)
        {
            var block = new Block(id);

            var result = PerformAction<UnassignBlockRoom, Block>(new UnassignBlockRoom(block));

            return new ActionResultToOkHttpActionResult<Block, BlockModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}
