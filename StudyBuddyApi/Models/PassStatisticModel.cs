using System;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class PassStatisticModel : ApiModel<PassStatistic, PassStatisticModel>, IPassStatistic
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public IPass Pass { get; set; }
        public decimal CostPerClass { get; set; }
        public int NumberOfClassesAttended { get; set; }
    }
}