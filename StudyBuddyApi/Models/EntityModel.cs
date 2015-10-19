using System;
using System.Collections.Generic;

namespace SpeedyDonkeyApi.Models
{
    public class EntityModel
    {
        public int Id { get; set; }
        public virtual List<TeacherModel> Teachers { get; set; }
        public virtual List<UserModel> RegisteredStudents { get; set; }
        public virtual DateTimeOffset StartTime { get; set; }
        public virtual DateTimeOffset EndTime { get; set; }
        public virtual string Name { get; set; }
    }
}