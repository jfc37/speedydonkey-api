using System;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class PassModel : ApiModel<Pass, PassModel>, IPass
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PassType { get; set; }
        public IUser Owner { get; set; }

        protected override string RouteName
        {
            get { return "PassApi"; }
        }

        protected override void AddChildUrls(HttpRequestMessage request, IUrlConstructor urlConstructor, Pass entity, PassModel model)
        {
            if (entity.Owner != null)
            {
                var userModel = new UserModel();
                model.Owner = (IUser) userModel.CreateModelWithOnlyUrl(request, urlConstructor, entity.Owner.Id);
            }
        }

        public override IApiModel<Pass> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, Pass entity,
            ICommonInterfaceCloner cloner)
        {
            if (entity is ClipPass)
            {
                var clipPass = (PassModel) base.CloneFromEntity(request, urlConstructor, entity, cloner);
                var clipPassModel = cloner.Clone<PassModel, ClipPassModel>(clipPass);
                clipPassModel.ClipsRemaining = ((ClipPass) entity).ClipsRemaining;
                return clipPassModel;
            }
            else
            {
                return base.CloneFromEntity(request, urlConstructor, entity, cloner);
            }
        }
    }

    public class ClipPassModel : PassModel, IClipPass
    {
        public int ClipsRemaining { get; set; }
    }
}