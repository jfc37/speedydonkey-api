using System;
using System.Collections.Generic;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class EventModel
    {
        public List<TeacherModel> Teachers { get; set; }
        public List<UserModel> RegisteredStudents { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public RoomModel Room { get; set; }
    }
}