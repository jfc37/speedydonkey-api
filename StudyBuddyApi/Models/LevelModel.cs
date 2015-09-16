using System;
using System.Collections.Generic;
using Models;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class LevelModel
    {
        public LevelModel()
        {
            
        }
        
        [JsonConstructor]
        public LevelModel(List<TeacherModel> teachers)
        {
            Teachers = teachers;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Room Room { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClassesInBlock { get; set; }
        public List<BlockModel> Blocks { get; set; }
        public int ClassMinutes { get; set; }
    }
}