using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class ClassModel
    {
        public ClassModel()
        {
            
        }
        
        [JsonConstructor]
        public ClassModel(List<TeacherModel> teachers)
        {
            Teachers = teachers;
        }

        public int Id { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public List<UserModel> RegisteredStudents { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Name { get; set; }
        public List<UserModel> ActualStudents { get; set; }
        public BlockModel Block { get; set; }
        public List<PassStatisticModel> PassStatistics { get; set; }
        public RoomModel Room { get; set; }
    }
}