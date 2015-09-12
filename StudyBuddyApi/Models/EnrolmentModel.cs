using System.Collections.Generic;
using System.Linq;

namespace SpeedyDonkeyApi.Models
{
    public class EnrolmentModel
    {
        public EnrolmentModel() { }

        public EnrolmentModel(params int[] blockIds)
        {
            BlockIds = blockIds.ToList();
        }

        public List<int> BlockIds { get; set; } 
    }
}