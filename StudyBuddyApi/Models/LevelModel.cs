﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using FluentNHibernate.Conventions;
using Models;
using Newtonsoft.Json;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class LevelModel : ApiModel<Level, LevelModel>, ILevel
    {
        public LevelModel()
        {
            
        }
        
        [JsonConstructor]
        public LevelModel(List<Teacher> teachers)
        {
            if (teachers != null)
                Teachers = teachers.ToList<ITeacher>();
        }

        public string Name { get; set; }
        public IRoom Room { get; set; }
        public ICollection<ITeacher> Teachers { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClassesInBlock { get; set; }
        public IList<IBlock> Blocks { get; set; }
        public int ClassMinutes { get; set; }

        protected override void AddChildrenToEntity(Level entity)
        {
            if (Teachers.IsNotNull() && Teachers.Any())
                entity.Teachers = Teachers;
        }

        protected override void AddChildrenToModel(Level entity, LevelModel model)
        {
            if (entity.Teachers != null)
            {
                model.Teachers = entity.Teachers.Select(x => new TeacherToTeacherModelMapping(x).Do()).ToList<ITeacher>();
            }
        }
    }
}