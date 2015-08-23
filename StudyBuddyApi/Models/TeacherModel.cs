using System;
using System.Collections.Generic;
using Common.Extensions;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class TeacherModel : ApiModel<Teacher, TeacherModel>, ITeacher
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public IUser User { get; set; }
        public ICollection<IClass> Classes { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return "{0} {1}".FormatWith(FirstName, Surname); } }
    }
}