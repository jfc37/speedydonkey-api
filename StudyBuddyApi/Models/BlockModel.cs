using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class BlockModel : ApiModel<Block, BlockModel>, IBlock
    {
        public ICollection<IUser> EnroledStudents { get; set; }
        public ILevel Level { get; set; }
        public IList<IClass> Classes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }


        protected override string RouteName
        {
            get { return "BlockApi"; }
        }

        protected override void AddChildrenToEntity(Block entity, ICommonInterfaceCloner cloner)
        {
            if (Level != null)
            {
                entity.Level = ((LevelModel) Level).ToEntity(cloner);
            }
        }
    }
}