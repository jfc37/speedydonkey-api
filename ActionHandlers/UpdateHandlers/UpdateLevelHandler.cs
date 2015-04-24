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
        private readonly IRepository<User> _userRepository;
        private readonly ICommonInterfaceCloner _cloner;

        public UpdateLevelHandler(
            IRepository<Level> repository, IRepository<User> userRepository, ICommonInterfaceCloner cloner)
        {
            _repository = repository;
            _userRepository = userRepository;
            _cloner = cloner;
        }

        public Level Handle(UpdateLevel action)
        {
            var originalEntity = _repository.Get(action.ActionAgainst.Id);
            _cloner.Copy(action.ActionAgainst, originalEntity);

            if (action.ActionAgainst.Teachers != null)
            {
                var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _userRepository.Get(teacher.Id)).Cast<IUser>().ToList();
                originalEntity.Teachers = actualTeachers;
            }

            return _repository.Update(originalEntity);

        }
    }
}
