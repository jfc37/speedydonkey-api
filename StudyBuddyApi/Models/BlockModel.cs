using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class BlockModel : IEntity
    {
        public BlockModel()
        {
        }

        [JsonConstructor]
        public BlockModel(List<TeacherModel> teachers)
        {
           Teachers = teachers;
        }

        public BlockModel(int id)
        {
            Id = id;
        }

        public List<TeacherModel> Teachers { get; set; }
        public List<UserModel> EnroledStudents { get; set; }
        public List<ClassModel> Classes { get; set; }
        public List<AnnouncementModel> Announcements { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public RoomModel Room { get; set; }
        public int NumberOfClasses { get; set; }
        public int MinutesPerClass { get; set; }
        public string Name { get; set; }
        public bool IsInviteOnly { get; set; }
        public int Id { get; set; }
        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Name), nameof(StartDate), nameof(EndDate));
        }
    }
}