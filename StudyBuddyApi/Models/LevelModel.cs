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

        protected override void AddChildUrls(HttpRequestMessage request, IUrlConstructor urlConstructor, Level entity, LevelModel model)
        {
            if (entity.Blocks != null)
            {
                var blockModel = new BlockModel();
                model.Blocks = entity.Blocks
                    .Select(x => (IBlock) blockModel.CreateModelWithOnlyUrl(request, urlConstructor, x.Id))
                    .ToList();
            }
        }
    }
}