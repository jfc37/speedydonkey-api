using System;
using System.Collections.Generic;
using Common;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class AnnouncementModel : IEntity
    {
        public AnnouncementModel()
        {
            
        }
        
        [JsonConstructor]
        public AnnouncementModel(List<BlockModel> receivers)
        {
           Receivers = receivers;
        }
        public string Message { get; set; }
        public List<BlockModel> Receivers { get; set; }
        public string Subject { get; set; }
        public bool NotifyAll { get; set; }
        public int Id { get; set; }
    }
}