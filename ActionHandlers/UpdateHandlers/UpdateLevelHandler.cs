using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateLevelHandler : IActionHandler<UpdateLevel, Level>
    {
        private readonly IRepository<Level> _repository;
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly ICommonInterfaceCloner _cloner;

        public UpdateLevelHandler(
            IRepository<Level> repository, IRepository<Teacher> teacherRepository, ICommonInterfaceCloner cloner)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
            _cloner = cloner;
        }

        public Level Handle(UpdateLevel action)
        {
            var originalEntity = _repository.Get(action.ActionAgainst.Id);
            _cloner.Copy(action.ActionAgainst, originalEntity);

            if (HasTeachersChanged(originalEntity.Teachers, action.ActionAgainst.Teachers))
            {
                var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _teacherRepository.Get(teacher.Id)).Cast<ITeacher>().ToList();
                originalEntity.Teachers = actualTeachers;
            }

            return _repository.Update(originalEntity);

        }

        private bool HasTeachersChanged(ICollection<ITeacher> orginal, ICollection<ITeacher> updated)
        {
            var orginalIds = orginal.Select(x => x.Id);
            var updatedIds = updated.Select(x => x.Id);
            var hasSameNumber = orginalIds.Count() == updatedIds.Count();
            var areSameIds = orginalIds.All(x => updatedIds.Contains(x));

            return !hasSameNumber || !areSameIds;
        }
    }
}
