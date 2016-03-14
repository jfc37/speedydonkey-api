using System;
using Common;

namespace Models
{
    public interface IDatabaseEntity
    {
        DateTime CreatedDateTime { get; set; }
        DateTime? LastUpdatedDateTime { get; set; }
        bool IsDeleted { get; set; }
    }

    public abstract class DatabaseEntity : IDatabaseEntity, IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}