using System.Linq;
using Action.Classes;
using Data.Repositories;
using Models;

namespace ActionHandlers.Classes
{
    public class ChangeClassTeachersHandler : IActionHandler<ChangeClassTeachers, Class>
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Teacher> _teacherRepository;

        public ChangeClassTeachersHandler(IRepository<Class> classRepository, IRepository<Teacher> teacherRepository)
        {
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
        }

        public Class Handle(ChangeClassTeachers action)
        {
            var theClass = _classRepository.Get(action.ActionAgainst.Id);
            var newTeacherIds = action.ActionAgainst.Teachers.Select(x => x.Id);
            var newClassTeachers = _teacherRepository.GetAll()
                .Where(x => newTeacherIds.Contains(x.Id))
                .ToList();

            theClass.Teachers = newClassTeachers;

            return _classRepository.Update(theClass);
        }
    }
}
