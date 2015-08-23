using System.Linq;
using Action;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateLevelHandler : IActionHandler<UpdateLevel, Level>
    {
        private readonly IRepository<Level> _repository;
        private readonly IRepository<Teacher> _teacherRepository;

        public UpdateLevelHandler(
            IRepository<Level> repository, IRepository<Teacher> teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
        }

        public Level Handle(UpdateLevel action)
        {
            var originalEntity = _repository.Get(action.ActionAgainst.Id);
            new CommonInterfaceCloner().Copy(action.ActionAgainst, originalEntity);

            if (originalEntity.Teachers.DoesNotHaveSameItems(action.ActionAgainst.Teachers))
            {
                var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _teacherRepository.Get(teacher.Id)).Cast<ITeacher>().ToList();
                originalEntity.Teachers = actualTeachers;
            }

            return _repository.Update(originalEntity);

        }
    }
}
