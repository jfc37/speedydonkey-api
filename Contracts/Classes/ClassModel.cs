using System.Collections.Generic;
using Contracts.Blocks;
using Contracts.Events;
using Contracts.Passes;
using Contracts.Teachers;
using Newtonsoft.Json;

namespace Contracts.Classes
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