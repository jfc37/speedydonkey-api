using System;
using System.Collections.Generic;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class EventModel
    {
        public List<TeacherModel> Teachers { get; set; }
        public List<UserModel> RegisteredStudents { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
    }
}