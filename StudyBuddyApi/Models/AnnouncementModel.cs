using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class AnnouncementModel : ApiModel<Announcement, AnnouncementModel>, IAnnouncement
    {
        public AnnouncementModel()
        {
            
        }
        
        [JsonConstructor]
        public AnnouncementModel(List<Block> receivers)
        {
            if (receivers != null)
                Receivers = receivers.OfType<IBlock>().ToList();
        }


        protected override void AddChildrenToEntity(Announcement entity)
        {
            if (Receivers != null && Receivers.Any())
                entity.Receivers = Receivers;
        }

        protected override void AddChildrenToModel(Announcement entity, AnnouncementModel model)
        {
            if (entity.Receivers != null)
            {
                model.Receivers = entity.Receivers.Select(x => (IBlock)new BlockModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList();
            }
        }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public string Message { get; set; }
        public ICollection<IBlock> Receivers { get; set; }
        public string Type { get; set; }
        public DateTime? ShowFrom { get; set; }
        public DateTime? ShowUntil { get; set; }
        public bool NotifyAll { get; set; }
    }
}