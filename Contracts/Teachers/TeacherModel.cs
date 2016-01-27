using System.Collections.Generic;
using Contracts.Classes;
using Contracts.Users;

namespace Contracts.Teachers
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public List<ClassModel> Classes { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName => @"{FirstName} {Surname}";
    }
}