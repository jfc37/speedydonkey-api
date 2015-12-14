using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class ClassModel : EventModel
    {
        public ClassModel()
        {
            
        }
        
        [JsonConstructor]
        public ClassModel(List<TeacherModel> teachers)
        {
            Teachers = teachers;
        }

        public BlockModel Block { get; set; }
        public List<PassStatisticModel> PassStatistics { get; set; }
    }
}