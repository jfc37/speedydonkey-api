﻿using Action;
using ActionHandlers.CreateHandlers.Strategies;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateBlockHandler : CreateEntityHandler<CreateBlock, Block>
    {
        private readonly IRepository<Level> _levelRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IBlockPopulatorStrategyFactory _blockPopulatorStrategyFactory;

        public CreateBlockHandler(
            IRepository<Block> repository, 
            IRepository<Level> levelRepository, 
            IRepository<Class> classRepository, 
            IRepository<Booking> bookingRepository, 
            IBlockPopulatorStrategyFactory blockPopulatorStrategyFactory) : base(repository)
        {
            _levelRepository = levelRepository;
            _classRepository = classRepository;
            _bookingRepository = bookingRepository;
            _blockPopulatorStrategyFactory = blockPopulatorStrategyFactory;
        }

        protected override void PreHandle(ICrudAction<Block> action)
        {
            var level = _levelRepository.Get(action.ActionAgainst.Level.Id);
            action.ActionAgainst.Name = level.Name;
            var populatorStrategy = _blockPopulatorStrategyFactory.GetStrategy(level);
            populatorStrategy.PopulateBlock(action.ActionAgainst, level);
        }

        protected override void PostHandle(ICrudAction<Block> action, Block result)
        {
            var classTime = result.StartDate;
            while (classTime < result.EndDate)
            {
                var nextClass = new Class
                {
                    StartTime = classTime,
                    EndTime = classTime.AddMinutes(result.Level.ClassMinutes),
                    Block = result,
                    Name = result.Name
                };
                CreateBookingForClass(nextClass, result.Level.Room);
                _classRepository.Create(nextClass);
                classTime = classTime.AddDays(7);
            }
        }

        private void CreateBookingForClass(Class nextClass, IRoom room)
        {
            var booking = new Booking
            {
                Event = nextClass,
                Room = room
            };
            _bookingRepository.Create(booking);
        }
    }
}
