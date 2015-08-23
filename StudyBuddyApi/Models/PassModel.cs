using System;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Controllers;

namespace SpeedyDonkeyApi.Models
{
    public class PassModel : ApiModel<Pass, PassModel>, IPass
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PassType { get; set; }

        public string PassNumber
        {
            get
            {
                return String.Format("{0}{1}", CreatedDateTime.ToString("yy"), Id.ToString("D4"));
            }
        }

        public string PaymentStatus { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public IUser Owner { get; set; }
        public IPassStatistic PassStatistic { get; set; }

        public bool Valid
        {
            get { return IsValid(); }
        }

        protected virtual string RouteName
        {
            get { return "PassApi"; }
        }
        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return StartDate <= today && today <= EndDate;
        }

        public override Pass ToEntity()
        {
            if (this is ClipPassModel)
            {
                var entity = new CommonInterfaceCloner().Clone<ClipPassModel, ClipPass>(this as ClipPassModel);
                AddChildrenToEntity(entity);
                return entity;
            }
            return base.ToEntity();
        }

        public override IApiModel<Pass> CloneFromEntity(HttpRequestMessage request, Pass entity)
        {
            if (entity is ClipPass)
            {
                var clipPass = (PassModel) base.CloneFromEntity(request, entity);
                var clipPassModel = new CommonInterfaceCloner().Clone<PassModel, ClipPassModel>(clipPass);
                clipPassModel.ClipsRemaining = ((ClipPass) entity).ClipsRemaining;
                return clipPassModel;
            }
            else
            {
                return base.CloneFromEntity(request, entity);
            }
        }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public string Note { get; set; }
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