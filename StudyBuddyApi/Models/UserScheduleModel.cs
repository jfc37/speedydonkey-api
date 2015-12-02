using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class UserScheduleModel
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }

        public IList<UserScheduleModel> ConvertFromUser(User entity)
        {
            var schedule = entity.Schedule.ToList();
            var upcomingSchedule = schedule.Select(x => new UserScheduleModel
            {
                EndTime = x.EndTime,
                StartTime = x.StartTime,
                EventId = x.Id,
                Name = x.Name
            })
            .Where(x => x.StartTime > DateTime.Now.AddHours(-1))
            .OrderBy(x => x.StartTime)
            .Take(10)
            .ToList();
            return upcomingSchedule;
        }
    }
}