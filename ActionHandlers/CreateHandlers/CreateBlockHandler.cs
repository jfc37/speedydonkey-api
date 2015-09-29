using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Actions;
using Common.Extensions;
using Data.CodeChunks;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateClassesForBlock : ICodeChunk<IEnumerable<Class>>
    {
        private readonly IRepository<Class> _repository;
        private readonly Block _block;

        public CreateClassesForBlock(IRepository<Class> repository, Block block)
        {
            _repository = repository;
            _block = block;
        }

        public IEnumerable<Class> Do()
        {
            var classTime = _block.StartDate;
            for (int classNumber = 1; classNumber <= _block.NumberOfClasses; classNumber++)
            {
                var nextClass = new Class
                {
                    StartTime = classTime,
                    EndTime = classTime.AddMinutes(_block.MinutesPerClass),
                    Block = _block,
                    Name = _block.Name + " - Week " + classNumber,
                    Teachers = new List<Teacher>(_block.Teachers),
                    CreatedDateTime = DateTime.Now
                };
                var createdClass = _repository.Create(nextClass);
                classTime = classTime.AddDays(7);

                yield return createdClass;
            }
        }
    }

    public class CreateBlockHandler : CreateEntityHandler<CreateBlock, Block>
    {
        private readonly IRepository<Class> _classRepository;

        public CreateBlockHandler(
            IRepository<Block> repository, 
            IRepository<Class> classRepository) : base(repository)
        {
            _classRepository = classRepository;
        }

        protected override void PreHandle(ICrudAction<Block> action)
        {
            base.PreHandle(action);
            action.ActionAgainst.EndDate = action.ActionAgainst.StartDate
                .AddMinutes(action.ActionAgainst.MinutesPerClass)
                .AddWeeks(action.ActionAgainst.NumberOfClasses - 1);
        }

        protected override void PostHandle(ICrudAction<Block> action, Block result)
        {
            result.Classes = new CreateClassesForBlock(_classRepository, result)
                .Do()
                .ToList();
        }
    }
    public class CreateNextBlockHandler : CreateEntityHandler<CreateNextBlock, Block>
    {
        private readonly IRepository<Block> _repository;
        private readonly IRepository<Class> _classRepository;

        public CreateNextBlockHandler(
            IRepository<Block> repository, 
            IRepository<Class> classRepository) : base(repository)
        {
            _repository = repository;
            _classRepository = classRepository;
        }

        protected override void PreHandle(ICrudAction<Block> action)
        {
            base.PreHandle(action);

            var previousBlock = _repository.Get(action.ActionAgainst.Id);

            action.ActionAgainst = new Block
            {
                CreatedDateTime = DateTime.Now,
                MinutesPerClass = previousBlock.MinutesPerClass,
                NumberOfClasses = previousBlock.NumberOfClasses,
                Name = previousBlock.Name,
                Teachers = new List<Teacher>(previousBlock.Teachers),
                StartDate = previousBlock.StartDate.AddWeeks(previousBlock.NumberOfClasses)
            };

            action.ActionAgainst.EndDate = action.ActionAgainst.StartDate
                .AddMinutes(action.ActionAgainst.MinutesPerClass)
                .AddWeeks(action.ActionAgainst.NumberOfClasses - 1);
        }

        protected override void PostHandle(ICrudAction<Block> action, Block result)
        {
            result.Classes = new CreateClassesForBlock(_classRepository, result)
                .Do()
                .ToList();
        }
    }
}
