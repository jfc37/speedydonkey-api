using System.Linq;
using Action;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace ActionHandlers.Blocks
{
    public class CreateBlockHandler : IActionHandler<CreateBlock, Block>
    {
        private readonly IRepository<Block> _blockRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Room> _roomRepository;

        public CreateBlockHandler(
            IRepository<Block> blockRepository, 
            IRepository<Class> classRepository,
            IRepository<Room> roomRepository)
        {
            _blockRepository = blockRepository;
            _classRepository = classRepository;
            _roomRepository = roomRepository;
        }

        public Block Handle(CreateBlock action)
        {
            action.ActionAgainst.EndDate = action.ActionAgainst.StartDate
                .AddMinutes(action.ActionAgainst.MinutesPerClass)
                .AddWeeks(action.ActionAgainst.NumberOfClasses - 1);

            action.ActionAgainst.StartDate = action.ActionAgainst.StartDate.ToUniversalTime();
            action.ActionAgainst.EndDate = action.ActionAgainst.EndDate.ToUniversalTime();

            var createdBlock = _blockRepository.Create(action.ActionAgainst);

            createdBlock.Classes = new CreateClassesForBlock(_classRepository, createdBlock)
                .Do()
                .ToList();

            if (action.ActionAgainst.Room.IsNotNull())
            {
                createdBlock.Room = _roomRepository.Get(action.ActionAgainst.Room.Id);
                createdBlock = new ClassRoomChangerForBlock(_classRepository, createdBlock, createdBlock.Room).Do();
                createdBlock = _blockRepository.Update(createdBlock);
            }


            return createdBlock;
        }
    }
}