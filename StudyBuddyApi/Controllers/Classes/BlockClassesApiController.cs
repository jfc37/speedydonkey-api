using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Extensions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers.Classes
{
    [RoutePrefix("api/blocks")]
    public class BlockClassesApiController : BaseApiController
    {
        private readonly IRepository<Block> _repository;

        public BlockClassesApiController(IRepository<Block> repository)
        {
            _repository = repository;
        }

        [Route("{blockId:int}/classes")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IEnumerable<ClassModel> Get(int blockId)
        {
            var block = _repository.Get(blockId);

            return block.IsNull()
                ? new List<ClassModel>()
                : block.Classes
                    .Select(x => x.ToModel())
                    .ToList();
        }
    }
}