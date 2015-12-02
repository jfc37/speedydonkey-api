using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace ActionHandlers.Blocks
{
    public class CreateNextBlockHandler : IActionHandler<CreateNextBlock, Block>
    {
        private readonly IRepository<Block> _blockRepository;
        private readonly IRepository<Class> _classRepository;

        public CreateNextBlockHandler(
            IRepository<Block> blockRepository, 
            IRepository<Class> classRepository)
        {
            _blockRepository = blockRepository;
            _classRepository = classRepository;
        }

        public Block Handle(CreateNextBlock action)
        {
            var previousBlock = _blockRepository.Get(action.ActionAgainst.Id);

            action.ActionAgainst = new Block
            {
                CreatedDateTime = DateTime.Now,
                MinutesPerClass = previousBlock.MinutesPerClass,
                NumberOfClasses = previousBlock.NumberOfClasses,
                Name = previousBlock.Name,
                Teachers = new List<Teacher>(previousBlock.Teachers),
                StartDate = previousBlock.StartDate.AddWeeks(previousBlock.NumberOfClasses),
                Room = previousBlock.Room,
                IsInviteOnly = previousBlock.IsInviteOnly
            };

            action.ActionAgainst.EndDate = action.ActionAgainst.StartDate
                .AddMinutes(action.ActionAgainst.MinutesPerClass)
                .AddWeeks(action.ActionAgainst.NumberOfClasses - 1);

            var createdBlock = _blockRepository.Create(action.ActionAgainst);

            createdBlock.Classes = new CreateClassesForBlock(_classRepository, createdBlock)
                .Do()
                .ToList();

            if (createdBlock.Room.IsNotNull())
            {
                createdBlock = new ClassRoomChangerForBlock(_classRepository, createdBlock, createdBlock.Room).Do();
            }

            return createdBlock;
        }
    }
}
