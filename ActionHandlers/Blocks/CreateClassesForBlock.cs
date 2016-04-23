using System;
using System.Collections.Generic;
using Data.CodeChunks;
using Data.Repositories;
using Models;

namespace ActionHandlers.Blocks
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
                    ClassCapacity = _block.ClassCapacity,
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
}