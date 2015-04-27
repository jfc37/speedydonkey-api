using System.Collections.Generic;
using System.Linq;
using Action;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateLevelHandler : CreateEntityHandler<CreateLevel, Level>
    {
        private readonly IRepository<User> _userRepository;

        public CreateLevelHandler(IRepository<Level> repository, IRepository<User> userRepository) : base(repository)
        {
            _userRepository = userRepository;
        }

        protected override void PreHandle(ICrudAction<Level> action)
        {
            var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _userRepository.Get(teacher.Id)).Cast<ITeacher>().ToList();
            action.ActionAgainst.Teachers = actualTeachers;
        }
    }
}
