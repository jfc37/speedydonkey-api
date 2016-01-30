using System;
using System.Collections.Generic;
using Common.Extensions;

namespace SpeedyDonkeyApi.Models
{
    public class EventModel
    {
        public List<TeacherModel> Teachers { get; set; }
        public List<UserModel> RegisteredStudents { get; set; }
        public List<UserModel> ActualStudents { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public RoomModel Room { get; set; }
        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Name), nameof(StartTime), nameof(EndTime));
        }
    }

    public class StandAloneEventModel : EventModel
    {
        public decimal Price { get; set; }
        public bool IsPrivate { get; set; }
    }
}