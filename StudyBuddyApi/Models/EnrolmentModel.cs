using System.Collections.Generic;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class EnrolmentModel
    {
        public int UserId { get; set; } 
        public IList<int> BlockIds { get; set; } 
        public IList<string> PassTypes { get; set; } 
    }
}