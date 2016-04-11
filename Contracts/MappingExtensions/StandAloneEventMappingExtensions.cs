using System.Linq;
using Common.Extensions;
using Contracts.Events;
using Models;

namespace Contracts.MappingExtensions
{
    public static class StandAloneEventMappingExtensions
    {
        public static StandAloneEvent ToEntity(this StandAloneEventModel instance)
        {
            if (instance.IsNull())
                return null;

            return new StandAloneEvent
            {
                Id = instance.Id,
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Price = instance.Price,
                IsPrivate = instance.IsPrivate
            };
        }

        public static EventForRegistrationModel ToEventForRegistrationModel(this StandAloneEvent instance, int userId)
        {
            if (instance.IsNull())
                return null;

            return new EventForRegistrationModel
            {
                Id = instance.Id,
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                Price = instance.Price,
                IsPrivate = instance.IsPrivate,
                IsAlreadyRegistered = instance.RegisteredStudents.Any(x => x.Id == userId)
            };
        }
    }
}