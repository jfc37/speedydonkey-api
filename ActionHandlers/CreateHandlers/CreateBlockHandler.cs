using System;
using System.Linq;
using Action;
using ActionHandlers.CreateHandlers.Strategies;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateBlockHandler : CreateEntityHandler<CreateBlock, Block>
    {
        private readonly IRepository<Level> _levelRepository;
        private readonly IBlockPopulatorStrategyFactory _blockPopulatorStrategyFactory;

        public CreateBlockHandler(
            IRepository<Block> repository,
            IRepository<Level> levelRepository,
            IBlockPopulatorStrategyFactory blockPopulatorStrategyFactory) : base(repository)
        {
            _levelRepository = levelRepository;
            _blockPopulatorStrategyFactory = blockPopulatorStrategyFactory;
        }

        protected override void PreHandle(ICreateAction<Block> action)
        {
            var level = _levelRepository.Get(action.ActionAgainst.Level.Id);
            var futureBlockAlreadyExists = level.Blocks.Any(x => x.StartDate > DateTime.Now.Date);
            if (futureBlockAlreadyExists)
            {
                ShouldCreateEntity = false;
                return;
            }

            var populatorStrategy = _blockPopulatorStrategyFactory.GetStrategy(level);
            populatorStrategy.PopulateBlock(action.ActionAgainst, level);
        }
    }
}
