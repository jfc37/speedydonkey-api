using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
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
                Teachers = teachers.OfType<ITeacher>().ToList();
        }

        public string Name { get; set; }
        public IRoom Room { get; set; }
        public IList<ITeacher> Teachers { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClassesInBlock { get; set; }
        public IList<IBlock> Blocks { get; set; }
        public int ClassMinutes { get; set; }

        protected override string RouteName
        {
            get { return "LevelApi"; }
        }

        protected override void AddChildrenToEntity(Level entity, ICommonInterfaceCloner cloner)
        {
            if (Teachers != null && Teachers.Any())
                entity.Teachers = Teachers;
        }

        protected override void AddChildrenToModel(Level entity, LevelModel model)
        {
            model.Teachers = entity.Teachers;
        }
    }
}