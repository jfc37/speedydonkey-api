using System;
using System.Collections.Generic;
using Common.Extensions;
using Contracts.Rooms;
using Contracts.Teachers;
using Contracts.Users;

namespace Contracts.Events
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
}