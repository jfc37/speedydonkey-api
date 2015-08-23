using System;
using System.Collections.Generic;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class EventModel : ApiModel<Event, EventModel>, IEvent
    {
        public ICollection<ITeacher> Teachers { get; set; }
        public ICollection<IUser> RegisteredStudents { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
    }
}