using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.ClassCheckIn
{
    public class RemoveStudentFromClassHandler : IActionHandler<RemoveStudentFromClass, Class>
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<User> _userRepository;

        public RemoveStudentFromClassHandler(IRepository<Class> classRepository, IRepository<User> userRepository)
        {
            _classRepository = classRepository;
            _userRepository = userRepository;
        }

        public Class Handle(RemoveStudentFromClass action)
        {
            var user = _userRepository.Get(action.ActionAgainst.ActualStudents.Single().Id);
            UpdatePass(user);
            var theClass = _classRepository.Get(action.ActionAgainst.Id);
            theClass.ActualStudents = theClass.ActualStudents ?? new List<User>();
            theClass.ActualStudents.Remove(user);
            _classRepository.Update(theClass);

            return theClass;
        }

        private void UpdatePass(User user)
        {
            var passToUse = user.GetPassToRefund();
            passToUse.RefundForClass();
        }
    }
}
