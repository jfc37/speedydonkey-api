using System;
using System.Collections.Generic;
using System.Linq;
using Data.CodeChunks;
using Models;

namespace ActionHandlers.CreateHandlers.Strategies
{
    public interface IBlockPopulatorStrategyFactory
    {
        IBlockPopulatorStrategy GetStrategy(Level level);
    }

    public class BlockPopulatorStrategyFactory : IBlockPopulatorStrategyFactory
    {
        public IBlockPopulatorStrategy GetStrategy(Level level)
        {
            if (level.Blocks.Any())
                return new LevelWithExistingBlockBlockPopulatorStrategy();

            return new FirstBlockInLevelBlockPopulatorStrategy();
        }
    }

    public interface IBlockPopulatorStrategy
    {
        void PopulateBlock(Block block, Level level);
    }

    public class FirstBlockInLevelBlockPopulatorStrategy : IBlockPopulatorStrategy
    {
        public void PopulateBlock(Block block, Level level)
        {
            block.Level = level;
            block.StartDate = level.StartTime;
            block.EndDate = new GetBlockEndDate(block.StartDate, level).Do();

            block.Teachers = new List<Teacher>(level.Teachers);
        }
    }

    public class LevelWithExistingBlockBlockPopulatorStrategy : IBlockPopulatorStrategy
    {
        public void PopulateBlock(Block block, Level level)
        {
            var latestBlock = level.Blocks
                .OrderByDescending(x => x.StartDate)
                .First();

            var lastClassStartTime = latestBlock.Classes.Max(x => x.StartTime);

            block.Level = level;
            block.StartDate = lastClassStartTime.AddDays(7);
            block.EndDate = new GetBlockEndDate(block.StartDate, level).Do();

            block.Teachers = new List<Teacher>(level.Teachers);
        }
    }
}