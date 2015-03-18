using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using NHibernate;

namespace SpeedyDonkeyApi.Models
{
    public class UserScheduleModel
    {
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }

        public IList<UserScheduleModel> ConvertFromUser(User entity)
        {
            var schedule = entity.Schedule.ToList();
            var upcomingSchedule = schedule.Select(x => new UserScheduleModel
            {
                EndTime = x.Event.EndTime,
                StartTime = x.Event.StartTime,
                EventId = x.Event.Id,
                Name = x.Event.Name
            })
            .Where(x => x.StartTime > DateTime.Now.AddHours(-1))
            .OrderBy(x => x.StartTime)
            .Take(10)
            .ToList();
            return upcomingSchedule;
        }
    }
}