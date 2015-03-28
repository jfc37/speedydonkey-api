using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.ClassCheckIn
{
    public class CheckStudentIntoClassHandler : IActionHandler<CheckStudentIntoClass, Class>
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<User> _userRepository;

        public CheckStudentIntoClassHandler(IRepository<Class> classRepository, IRepository<User> userRepository)
        {
            _classRepository = classRepository;
            _userRepository = userRepository;
        }

        public Class Handle(CheckStudentIntoClass action)
        {
            var user = _userRepository.Get(action.ActionAgainst.ActualStudents.Single().Id);
            UpdatePass(user);
            var theClass = _classRepository.Get(action.ActionAgainst.Id);
            AddStudentToClassAttendance(theClass, user);
            _classRepository.Update(theClass);

            return theClass;
        }

        private static void AddStudentToClassAttendance(Class theClass, User user)
        {
            if (user.FullName == "Full Swing Visitor")
                theClass.NumberOfVisitors++;
            else
            {
                theClass.ActualStudents = theClass.ActualStudents ?? new List<IUser>();
                theClass.ActualStudents.Add(user);
            }

        }

        private void UpdatePass(User user)
        {
            var passToUse = user.GetPassToUse();
            ((Pass) passToUse).PayForClass(); 
        }
    }
}
