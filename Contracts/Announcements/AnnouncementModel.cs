using System.Collections.Generic;
using Common;
using Common.Extensions;
using Contracts.Blocks;
using Newtonsoft.Json;

namespace Contracts.Announcements
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
        
        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Subject), nameof(Message), nameof(NotifyAll), nameof(Receivers));
        }
    }
}