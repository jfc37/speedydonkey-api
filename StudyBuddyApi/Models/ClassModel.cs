﻿using System;
using System.Collections.Generic;
using Models;
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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public List<UserModel> ActualStudents { get; set; }
        public BlockModel Block { get; set; }
        public List<PassStatisticModel> PassStatistics { get; set; }
    }
}