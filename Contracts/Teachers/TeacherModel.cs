using System.Collections.Generic;
using Common.Extensions;
using Contracts.Classes;
using Contracts.Users;

namespace Contracts.Teachers
{
    public class TeacherModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherModel"/> class.
        /// </summary>
        public TeacherModel()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherModel"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public TeacherModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public UserModel User { get; set; }
        public List<ClassModel> Classes { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{FirstName} {Surname}";
        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(FirstName), nameof(Surname));
        }
    }
}