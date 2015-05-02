using System;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class PassTemplateModel : ApiModel<PassTemplate, PassTemplateModel>, IPassTemplate
    {
        protected override string RouteName
        {
            get { return "PassTemplateApi"; }
        }

        public string Description { get; set; }
        public string PassType { get; set; }
        public decimal Cost { get; set; }
        public int WeeksValidFor { get; set; }
        public int ClassesValidFor { get; set; }
        public bool AvailableForPurchase { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}