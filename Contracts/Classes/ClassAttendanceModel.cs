using System.Collections.Generic;
using System.Linq;
using Contracts.Users;
using Models;

namespace Contracts.Classes
{
    public class ClassAttendanceModel : IEntityView<Class, UserModel>
    {
        public IEnumerable<UserModel> ConvertFromEntity(Class theClass)
        {
            var attendance = new List<UserModel>();
            if (theClass.ActualStudents == null)
                return attendance;

            foreach (var student in theClass.ActualStudents)
            {
                student.Passes = student.Passes.Where(x => x.IsValid()).ToList();
                var studentModel = student.ToModel();
                attendance.Add(studentModel);
            }
            return attendance;
        }
    }
}