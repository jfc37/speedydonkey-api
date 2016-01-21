using System.Collections.Generic;
using System.Linq;
using Contracts.Users;
using Models;

namespace Contracts.Classes
{
    public class ClassRegisterModel : IEntityView<Class, UserModel>
    {
        public IEnumerable<UserModel> ConvertFromEntity(Class theClass)
        {
            var classRoll = new List<UserModel>();
            if (theClass.RegisteredStudents == null)
                return classRoll;

            foreach (var student in theClass.RegisteredStudents)
            {
                student.Passes = student.Passes.Where(x => x.IsValid()).ToList();
                var studentModel = student.ToModel();
                classRoll.Add(studentModel);
            }
            return classRoll;
        }
    }
}