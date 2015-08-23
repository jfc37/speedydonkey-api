using System;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpeedyDonkeyApi.Models
{
    public class ActivityLogModel : ApiModel<ActivityLog, ActivityLogModel>, IActivityLog
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public Guid Session { get; set; }
        public int PerformingUserId { get; set; }
        public DateTime DateTimeStamp { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityGroup ActivityGroup { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityType ActivityType { get; set; }
        public string ActivityText { get; set; }
    }
}