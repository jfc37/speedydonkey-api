using System.Linq;
using Action;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class SetAsTeacherHandler : IActionHandler<SetAsTeacher, User>
    {
        private readonly IRepository<User> _repository;

        public SetAsTeacherHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Handle(SetAsTeacher action)
        {
            var userToMakeTeacher = _repository.Get(action.ActionAgainst.Id);
            userToMakeTeacher.TeachingConcerns = new TeachingConcerns();
            _repository.Update(userToMakeTeacher);

            return userToMakeTeacher;
        }
    }
}
