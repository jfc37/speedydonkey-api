using System.Collections.Generic;
using System.Linq;
using Action;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateClassHandler : IActionHandler<UpdateClass, Class>
    {
        private readonly IRepository<Class> _repository;
        private readonly IRepository<User> _userRepository;

        public UpdateClassHandler(
            IRepository<Class> repository, IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public Class Handle(UpdateClass action)
        {
            var theClass = _repository.Get(action.ActionAgainst.Id);
            theClass.Name = action.ActionAgainst.Name;
            theClass.StartTime = action.ActionAgainst.StartTime;
            theClass.EndTime = action.ActionAgainst.EndTime;

            if (HasTeachersChanged(theClass.Teachers, action.ActionAgainst.Teachers))
            {
                var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _userRepository.Get(teacher.Id)).Cast<IUser>().ToList();
                theClass.Teachers = actualTeachers;
            }
            _repository.Update(theClass);
            return theClass;
        }

        private bool HasTeachersChanged(IList<IUser> orginal, IList<IUser> updated)
        {
            var orginalIds = orginal.Select(x => x.Id);
            var updatedIds = updated.Select(x => x.Id);
            var hasSameNumber = orginalIds.Count() == updatedIds.Count();
            var areSameIds = orginalIds.All(x => updatedIds.Contains(x));

            return !hasSameNumber || !areSameIds;
        }
    }
}
