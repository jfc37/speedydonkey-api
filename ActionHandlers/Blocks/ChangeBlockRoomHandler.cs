using Action.Blocks;
using Data.Repositories;
using Models;

namespace ActionHandlers.Blocks
{
    public class ChangeBlockRoomHandler : IActionHandler<ChangeBlockRoom, Block>
    {
        private readonly IRepository<Block> _blockRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Room> _roomRepository;

        public ChangeBlockRoomHandler(IRepository<Block> blockRepository, IRepository<Class> classRepository, IRepository<Room> roomRepository)
        {
            _blockRepository = blockRepository;
            _classRepository = classRepository;
            _roomRepository = roomRepository;
        }

        public Block Handle(ChangeBlockRoom action)
        {
            var block = _blockRepository.Get(action.ActionAgainst.Id);
            var room = _roomRepository.Get(action.ActionAgainst.Room.Id);

            block = new ClassRoomChangerForBlock(_classRepository, block, room).Do();

            block.Room = room;
            return _blockRepository.Update(block);
        }
    }
}