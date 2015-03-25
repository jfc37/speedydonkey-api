﻿using System.Collections.Generic;
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
            var theClass = _classRepository.Get(action.ActionAgainst.Id);
            theClass.ActualStudents = theClass.ActualStudents ?? new List<IUser>();
            theClass.ActualStudents.Add(user);
            _classRepository.Update(theClass);

            return theClass;
        }
    }
}