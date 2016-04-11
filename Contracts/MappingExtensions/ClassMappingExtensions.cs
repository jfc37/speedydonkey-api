using Common.Extensions;
using Contracts.Classes;
using Models;

namespace Contracts.MappingExtensions
{
    public static class ClassMappingExtensions
    {
        public static Class ToEntity(this ClassModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Class
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                ClassCapacity = instance.ClassCapacity,
                ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Block = instance.Block.ToEntity(),
                PassStatistics = instance.PassStatistics.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Id = instance.Id
            };
        }
    }
}