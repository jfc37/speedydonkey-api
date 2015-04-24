using System.Collections.Generic;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class TeachingConcernsModel : ApiModel<TeachingConcerns, TeachingConcernsModel>, ITeachingConcerns
    {
        protected override string RouteName
        {
            get { return "TeachingConcernsApi"; }
        }

        public IList<IEvent> EventsRun { get; set; }
    }
}