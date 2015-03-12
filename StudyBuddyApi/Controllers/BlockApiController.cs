﻿using System.Net.Http;
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
    public class BlockApiController : GenericController<BlockModel, Block>
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

        public HttpResponseMessage Post(int levelId)
        {
            return Post(new BlockModel {Level = new LevelModel {Id = levelId}}, x => new CreateBlock(x));
        }
    }
}
