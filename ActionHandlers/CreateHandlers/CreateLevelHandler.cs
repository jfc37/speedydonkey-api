using System.Linq;
using Action;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateLevelHandler : CreateEntityHandler<CreateLevel, Level>
    {
        private readonly IRepository<Teacher> _teacherRepository;

        public CreateLevelHandler(IRepository<Level> repository, IRepository<Teacher> teacherRepository)
            : base(repository)
        {
            _teacherRepository = teacherRepository;
        }

        protected override void PreHandle(ICrudAction<Level> action)
        {
            action.ActionAgainst.Teachers = _teacherRepository.GetAll()
                .Where(x => action.ActionAgainst.Teachers.Any(y => y.Id == x.Id))
                .ToList();
        }
    }
}
