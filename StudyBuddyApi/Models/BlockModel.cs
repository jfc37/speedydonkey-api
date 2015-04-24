using System;
using System.Collections.Generic;
using Common;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class BlockModel : ApiModel<Block, BlockModel>, IBlock
    {
        public IList<IUser> Teachers { get; set; }
        public ICollection<IUser> EnroledStudents { get; set; }
        public ILevel Level { get; set; }
        public ICollection<IClass> Classes { get; set; }
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