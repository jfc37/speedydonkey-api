using System.Collections.Generic;
using System.Linq;
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
            block.EndDate = level.StartTime.AddDays(level.ClassesInBlock * 7);

            block.Teachers = new List<IUser>(level.Teachers);
        }
    }

    public class LevelWithExistingBlockBlockPopulatorStrategy : IBlockPopulatorStrategy
    {
        public void PopulateBlock(Block block, Level level)
        {
            var latestBlock = level.Blocks
                .OrderByDescending(x => x.StartDate)
                .First();
            block.Level = level;
            block.StartDate = latestBlock.EndDate.AddDays(7);
            block.EndDate = block.StartDate.AddDays(level.ClassesInBlock * 7);

            block.Teachers = new List<IUser>(level.Teachers);
        }
    }
}