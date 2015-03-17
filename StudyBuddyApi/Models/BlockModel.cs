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


        protected override string RouteName
        {
            get { return "BlockApi"; }
        }

        //protected override void AddChildUrls(HttpRequestMessage request, IUrlConstructor urlConstructor, Block entity, BlockModel model)
        //{
        //    if (entity.Level != null)
        //    {
        //        model.Level = (ILevel)new LevelModel().CreateModelWithOnlyUrl(request, urlConstructor, entity.Level.Id);
        //    }

        //    if (entity.EnroledStudents != null)
        //    {
        //        var userModel = new UserModel();
        //        model.EnroledStudents = entity.EnroledStudents
        //            .Select(x => (IUser)userModel.CreateModelWithOnlyUrl(request, urlConstructor, x.Id))
        //            .ToList();
        //    }

        //    if (entity.Classes != null)
        //    {
        //        var classModel = new ClassModel();
        //        model.Classes = entity.Classes
        //            .Select(x => (IClass)classModel.CreateModelWithOnlyUrl(request, urlConstructor, x.Id))
        //            .ToList();
        //    }
        //}

        protected override void AddFullChild(HttpRequestMessage request, IUrlConstructor urlConstructor, Block entity, BlockModel model,
            ICommonInterfaceCloner cloner)
        {
            if (entity.Level != null)
            {
                model.Level = (ILevel)new LevelModel().CloneFromEntity(request, urlConstructor, (Level)entity.Level, cloner, false);
            }
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