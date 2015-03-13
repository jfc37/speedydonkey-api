﻿using System;
using System.Collections.Generic;
using System.Linq;
using Models;

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
            return entity.Schedule.Select(x => new UserScheduleModel
            {
                EndTime = x.Event.EndTime,
                StartTime = x.Event.StartTime,
                EventId = x.Event.Id,
                Name = ((Class) x.Event).Block.Level.Name
            }).ToList();
        }
    }
}