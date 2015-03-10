using System.Net.Http;
using Action;
using ActionHandlers;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class BlockApiController : GenericController<BlockModel, Block>
    {
        public BlockApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<Block> repository) : base(actionHandlerOverlord, urlConstructor, repository)
        {
        }

        public HttpResponseMessage Post(int levelId)
        {
            return Post(new BlockModel {Level = new LevelModel {Id = levelId}}, x => new CreateBlock(x));
        }
    }
}
