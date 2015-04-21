using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class GenerateBlocksForAllLevelsHandler : IActionHandler<GenerateBlocksForAllLevels, Block>
    {
        private readonly IRepository<Level> _repository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public GenerateBlocksForAllLevelsHandler(IRepository<Level> repository, IActionHandlerOverlord actionHandlerOverlord)
        {
            _repository = repository;
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        public Block Handle(GenerateBlocksForAllLevels action)
        {
            var levels = _repository.GetAll();
            foreach (var level in levels)
            {
                var generateBlock = new CreateBlock(new Block {Level = new Level {Id = level.Id}});
                _actionHandlerOverlord.HandleAction<CreateBlock, Block>(generateBlock);
            }

            return action.ActionAgainst;
        }
    }
}
