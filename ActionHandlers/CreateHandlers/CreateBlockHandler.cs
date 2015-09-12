using System;
using System.Collections.Generic;
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
            var timeSpan = result.Level.EndTime.TimeOfDay.Subtract(result.Level.StartTime.TimeOfDay);
            for (int classNumber = 1; classNumber <= action.ActionAgainst.Level.ClassesInBlock; classNumber++)
            {
                var nextClass = new Class
                {
                    StartTime = classTime,
                    EndTime = classTime.AddMinutes(timeSpan.TotalMinutes),
                    Block = result,
                    Name = result.Name + " - Week " + classNumber,
                    Teachers = new List<Teacher>(result.Level.Teachers),
                    CreatedDateTime = DateTime.Now
                };
                CreateBookingForClass(nextClass, result.Level.Room);
                _classRepository.Create(nextClass);
                classTime = classTime.AddDays(7);
            }
        }

        private void CreateBookingForClass(Class nextClass, Room room)
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
