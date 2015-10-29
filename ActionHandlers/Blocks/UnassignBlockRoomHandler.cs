using Action.Blocks;
using Data.Repositories;
using Models;

namespace ActionHandlers.Blocks
{
    public class UnassignBlockRoomHandler : IActionHandler<UnassignBlockRoom, Block>
    {
        private readonly IRepository<Block> _blockRepository;
        private readonly IRepository<Class> _classRepository;

        public UnassignBlockRoomHandler(IRepository<Block> blockRepository, IRepository<Class> classRepository)
        {
            _blockRepository = blockRepository;
            _classRepository = classRepository;
        }

        public Block Handle(UnassignBlockRoom action)
        {
            var block = _blockRepository.Get(action.ActionAgainst.Id);

            block = new ClassRoomRemoverForBlock(_classRepository, block).Do();

            block.Room = null;
            return _blockRepository.Update(block);
        }
    }
}