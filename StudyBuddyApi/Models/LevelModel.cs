using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class LevelModel : ApiModel<Level, LevelModel>, ILevel
    {
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
    }
}