using System;
using System.Collections.Generic;
using System.Linq;
using Action.Classes;
using Data.Repositories;
using Models;

namespace ActionHandlers.Classes
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
            var pass = UpdatePass(user);
            var theClass = _classRepository.Get(action.ActionAgainst.Id);
            pass.PassStatistic.Pass = pass;
            AddStudentToClassAttendance(theClass, user, pass.PassStatistic);
            _classRepository.Update(theClass);

            return theClass;
        }

        private void AddStudentToClassAttendance(Class theClass, User user, PassStatistic passStatistic)
        {
            theClass.ActualStudents = theClass.ActualStudents ?? new List<User>();
            theClass.ActualStudents.Add(user);
            theClass.PassStatistics = theClass.PassStatistics ?? new List<PassStatistic>();
            theClass.PassStatistics.Add(passStatistic);
        }

        private Pass UpdatePass(User user)
        {
            var passToUse = user.GetPassToUse();
            passToUse.PayForClass();

            if (passToUse is ClipPass)
                MakeNextPassValid(user);

            return passToUse;
        }

        private void MakeNextPassValid(User user)
        {
            var nextPass = user.Passes
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
