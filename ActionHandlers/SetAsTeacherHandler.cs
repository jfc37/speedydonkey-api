using System;
using System.Collections.Generic;
using Actions;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class SetAsTeacherHandler : IActionHandler<SetAsTeacher, Teacher>
    {
        private readonly ITeacherStudentConverter _teacherStudentConverter;

        public SetAsTeacherHandler(ITeacherStudentConverter teacherStudentConverter)
        {
            _teacherStudentConverter = teacherStudentConverter;
        }

        public Teacher Handle(SetAsTeacher action)
        {
            return _teacherStudentConverter.ToTeacher(action.ActionAgainst.Id);
        }
    }

}
