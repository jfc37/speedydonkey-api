using System.Linq;
using Common.Extensions;
using Contracts.Events;
using Models;

namespace Contracts.MappingExtensions
{
    public static class StandAloneEventMappingExtensions
    {
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