using Models;

namespace SpeedyDonkeyApi.Models
{
    public class ReferenceDataModel : ApiModel<ReferenceData, ReferenceDataModel>, IReferenceData
    {
        protected override string RouteName
        {
            get { return "ReferenceDataApi"; }
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}