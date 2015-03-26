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
        public string PaymentStatus { get; set; }
        public IUser Owner { get; set; }

        protected override string RouteName
        {
            get { return "PassApi"; }
        }
        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return StartDate >= today && today <= EndDate;
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
        public override bool IsValid()
        {
            return ClipsRemaining > 0 && base.IsValid();
        }
    }
}