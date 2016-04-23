using System.Linq;
using Common.Extensions;
using Contracts.Blocks;
using Models;

namespace Contracts.MappingExtensions
{
    public static class BlockMappingExtensions
    {
        public static Block ToEntity(this BlockModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Block
            {
                Announcements = instance.Announcements.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                ClassCapacity = instance.ClassCapacity,
                Classes = instance.Classes.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                EnroledStudents = instance.EnroledStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Name = instance.Name,
                Id = instance.Id,
                MinutesPerClass = instance.MinutesPerClass,
                NumberOfClasses = instance.NumberOfClasses,
                Room = instance.Room.ToEntity(),
                IsInviteOnly = instance.IsInviteOnly
            };
        }

        public static BlockModel ToModel(this Block instance)
        {
            if (instance.IsNull())
                return null;

            return new BlockModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Classes = instance.Classes.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                EnroledStudents = instance.EnroledStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Name = instance.Name,
                Id = instance.Id,
                ClassCapacity = instance.ClassCapacity,
                MinutesPerClass = instance.MinutesPerClass,
                NumberOfClasses = instance.NumberOfClasses,
                Room = instance.Room.ToStripedModel(),
                IsInviteOnly = instance.IsInviteOnly
            };
        }

        public static BlockModel ToStripedModel(this Block instance)
        {
            if (instance.IsNull())
                return null;

            return new BlockModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Name = instance.Name,
                Id = instance.Id,
                ClassCapacity = instance.ClassCapacity,
                MinutesPerClass = instance.MinutesPerClass,
                NumberOfClasses = instance.NumberOfClasses,
                Room = instance.Room.ToStripedModel(),
                IsInviteOnly = instance.IsInviteOnly
            };
        }

        public static BlockForRegistrationModel ToBlockForRegistrationModel(this Block instance, int userId)
        {
            if (instance.IsNull())
                return null;

            return new BlockForRegistrationModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Name = instance.Name,
                Id = instance.Id,
                ClassCapacity = instance.ClassCapacity,
                MinutesPerClass = instance.MinutesPerClass,
                NumberOfClasses = instance.NumberOfClasses,
                Room = instance.Room.ToStripedModel(),
                IsInviteOnly = instance.IsInviteOnly,
                IsAlreadyRegistered = instance.EnroledStudents.Any(x => x.Id == userId),
                SpacesAvailable = instance.ClassCapacity - instance.EnroledStudents.Count
            };
        }
    }
}