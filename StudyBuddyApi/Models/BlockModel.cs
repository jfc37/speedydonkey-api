using System;
using System.Collections.Generic;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class BlockModel : ApiModel<Block, BlockModel>, IBlock
    {
        public IList<IUser> EnroledStudents { get; set; }
        public ILevel Level { get; set; }
        public IList<IClass> Classes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        protected override string RouteName
        {
            get { return "BlockApi"; }
        }

        protected override void AddChildUrls(HttpRequestMessage request, IUrlConstructor urlConstructor, Block entity, BlockModel model)
        {
            if (entity.Level != null)
            {
                model.Level = (ILevel) new LevelModel().CreateModelWithOnlyUrl(request, urlConstructor, entity.Level.Id);
            }
        }
    }
}