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
        public decimal Cost { get; set; }
        public IUser Owner { get; set; }
        public IPassStatistic PassStatistic { get; set; }

        public bool Valid
        {
            get { return IsValid(); }
        }

        protected override string RouteName
        {
            get { return "PassApi"; }
        }
        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return StartDate >= today && today <= EndDate;
        }

        public override Pass ToEntity(ICommonInterfaceCloner cloner)
        {
            if (this is ClipPassModel)
            {
                var entity = cloner.Clone<ClipPassModel, ClipPass>(this as ClipPassModel);
                AddChildrenToEntity(entity, cloner);
                return entity;
            }
            return base.ToEntity(cloner);
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

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
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