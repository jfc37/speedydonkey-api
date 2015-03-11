using System;
using System.Net.Http;
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
    }
}