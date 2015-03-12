﻿using System;
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
        private readonly IRepository<Class> _classRepository;
        private readonly IBlockPopulatorStrategyFactory _blockPopulatorStrategyFactory;

        public CreateBlockHandler(
            IRepository<Block> repository, 
            IRepository<Level> levelRepository, 
            IRepository<Class> classRepository, 
            IBlockPopulatorStrategyFactory blockPopulatorStrategyFactory) : base(repository)
        {
            _levelRepository = levelRepository;
            _classRepository = classRepository;
            _blockPopulatorStrategyFactory = blockPopulatorStrategyFactory;
        }

        protected override void PreHandle(ICreateAction<Block> action)
        {
            var level = _levelRepository.Get(action.ActionAgainst.Level.Id);
            //TODO: This check should be a validation error
            var futureBlockAlreadyExists = level.Blocks.Any(x => x.StartDate > DateTime.Now.Date);
            if (futureBlockAlreadyExists)
            {
                ShouldCreateEntity = false;
                return;
            }

            var populatorStrategy = _blockPopulatorStrategyFactory.GetStrategy(level);
            populatorStrategy.PopulateBlock(action.ActionAgainst, level);
        }

        protected override void PostHandle(ICreateAction<Block> action, Block result)
        {
            var classTime = result.StartDate;
            while (classTime < result.EndDate)
            {
                var nextClass = new Class
                {
                    StartTime = classTime,
                    EndTime = classTime.AddMinutes(result.Level.ClassMinutes),
                    Block = result
                };
                _classRepository.Create(nextClass);
                classTime = classTime.AddDays(7);
            }
        }
    }
}