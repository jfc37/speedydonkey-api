using System.Collections.Generic;
using Common.Extensions;

namespace SpeedyDonkeyApi.Models
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public List<ClassModel> Classes { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return "{0} {1}".FormatWith(FirstName, Surname); } }
        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(FirstName), nameof(Surname));
        }

    }
}