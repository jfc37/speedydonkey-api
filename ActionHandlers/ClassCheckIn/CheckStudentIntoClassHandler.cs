using System;
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

        private void AddStudentToClassAttendance(Class theClass, User user)
        {
            theClass.ActualStudents = theClass.ActualStudents ?? new List<IUser>();
            theClass.ActualStudents.Add(user);
        }

        private void UpdatePass(User user)
        {
            var passToUse = (Pass)user.GetPassToUse();
            passToUse.PayForClass();

            if (passToUse is ClipPass)
                MakeNextPassValid(user);
        }

        private void MakeNextPassValid(User user)
        {
            var nextPass = user.Passes
                .OfType<Pass>()
                .Where(x => x.IsFuturePass())
                .OrderBy(x => x.StartDate)
                .FirstOrDefault();

            if (nextPass != null)
            {
                var expiryPeriod = nextPass.EndDate.Subtract(nextPass.StartDate);
                nextPass.StartDate = DateTime.Now.Date;
                nextPass.EndDate = nextPass.StartDate.Add(expiryPeriod);
            }
        }
    }
}
